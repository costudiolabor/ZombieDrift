using System.Linq;
using Project;
using UnityEngine;

namespace Gameplay {
	public class ConstructState : State {
		private readonly ContentCreationService _contentCreationService;
		private readonly ProjectCache _projectCache;
		private readonly GameplayCache _gameplayCache;
		private readonly MoneyWallet _moneyWallet;
		private readonly ComboSystem _comboSystem;
		private readonly CarsConfig _carsConfig;
		private readonly ZombieStorage _zombieStorage;
		private readonly PauseService _pauseService;
		private readonly BotNavigation _botSystem;
		private readonly EnemyPointerSystem _enemyPointerSystem;
		private readonly StagesConfig _stagesConfig;
		private readonly GameProcess _gameProcess;
		private readonly CameraSystem _cameraSystem;
		private readonly StateSwitcher _stateSwitcher;
		private readonly VehicleController _vehicleController;
		private readonly VehicleDestroyer _vehicleDestroyer;
		private readonly FlyingRewardSystem _flyingRewardSystem;
		private readonly GameplayHudPresenter _gameplayHudPresenter;

		public ConstructState(
				StateSwitcher stateSwitcher,
				ContentCreationService contentCreationService,
				CameraSystem cameraSystem,
				GameProcess gameProcess,
				VehicleController vehicleController,
				VehicleDestroyer vehicleDestroyer,
				FlyingRewardSystem flyingRewardSystem,
				GameplayHudPresenter gameplayHudPresenter,
				BotNavigation botSystem,
				EnemyPointerSystem enemyPointerSystem,
				StagesConfig stagesConfig,
				ProjectCache projectCache,
				GameplayCache gameplayCache,
				MoneyWallet moneyWallet,
				ComboSystem comboSystem,
				CarsConfig carsConfig,
				ZombieStorage zombieStorage,
				PauseService pauseService
		) : base(stateSwitcher) {
			_stateSwitcher = stateSwitcher;
			_contentCreationService = contentCreationService;
			_cameraSystem = cameraSystem;
			_gameProcess = gameProcess;
			_vehicleController = vehicleController;
			_vehicleDestroyer = vehicleDestroyer;
			_flyingRewardSystem = flyingRewardSystem;
			_gameplayHudPresenter = gameplayHudPresenter;
			_projectCache = projectCache;
			_gameplayCache = gameplayCache;
			_moneyWallet = moneyWallet;
			_comboSystem = comboSystem;
			_carsConfig = carsConfig;
			_zombieStorage = zombieStorage;
			_pauseService = pauseService;
			_botSystem = botSystem;
			_enemyPointerSystem = enemyPointerSystem;
			_stagesConfig = stagesConfig;
		}

		public override void Enter() {
			Debug.Log("Construct");

			CalculateCombo();
			LoadGameplayCache();
			CreateGameplayObjects();
			SnapCameraToCar();
			InitializeGameplay();
			InitializePause();

			var mapsCount = _gameplayCache.mapsCount;
			var mapIndex = _gameplayCache.mapIndex;
			SetStageNumber(_projectCache.stageIndex, mapIndex, mapsCount);

			if (mapIndex == 0)
				SwitchToMenuState();
			else
				SwitchToHowToPlayState();
		}
		private void InitializePause() {
			_pauseService.Register(_vehicleController);
			_pauseService.Register(_flyingRewardSystem);
		}

		private void CalculateCombo() {
			var purchasedCars = _projectCache.purchasedCars;
			float comboMultiplier = 0;
			float comboDelay = 0;

			foreach (var carIndex in purchasedCars) {
				var purchasedCar = _carsConfig.cars[carIndex];
				comboMultiplier += purchasedCar.comboMultiplier;
				comboDelay += purchasedCar.comboDelay;
			}

			_comboSystem.comboMultiplier = comboMultiplier;
			_comboSystem.comboDelay = comboDelay;
			_comboSystem.Reset();
		}

		private void CreateGameplayObjects() {
			var stageIndex = _projectCache.stageIndex;
			var currentCarIndex = _projectCache.selectedCarIndex;
			var mapIndex = _gameplayCache.mapIndex;
			var map = _contentCreationService.CreateMap(stageIndex, mapIndex);
			map.navMeshSurface.BuildNavMesh();
			_gameplayCache.map = map;
			_gameplayCache.car = _contentCreationService.CreateCar(currentCarIndex, map.startPoint);
			//_gameplayCache.zombies = _contentCreationService.CreateZombies(map.zombieSpawnPoints);
			var zombies = _contentCreationService.CreateZombies(map.zombieSpawnPoints);
			_zombieStorage.AddNewRange(zombies);
			//
		}

		private void LoadGameplayCache() {
			var stageIndex = _projectCache.stageIndex;
			_gameplayCache.mapsCount = _stagesConfig.stages[stageIndex].count;
		}

		private void SetStageNumber(int stageIndex, int mapIndex, int mapsCount) {
			_gameplayHudPresenter.stageIndex = stageIndex;
			_gameplayHudPresenter.mapIndex = new Vector2Int(mapIndex, mapsCount);
			_gameplayHudPresenter.moneyCount = _moneyWallet.count;
		}

		private void SnapCameraToCar() =>
				_cameraSystem.target = _gameplayCache.car.transform;

		private void InitializeGameplay() {
			var car = _gameplayCache.car;
			car.Initialize();

			_vehicleController.SetCar(car);
			_vehicleDestroyer.SetCar(car);

		//	var zombiesArray = _gameplayCache.zombies;
			_botSystem.Initialize(_zombieStorage, car.transform);
			_gameProcess.Initialize(car, _zombieStorage);
			_enemyPointerSystem.SetNewData(_zombieStorage.ToArray(), car.transform);
		}

		private void SwitchToMenuState() =>
				_stateSwitcher.SetState<MenuState>();

		private void SwitchToHowToPlayState() =>
				_stateSwitcher.SetState<GetReadyState>();
	}
}
