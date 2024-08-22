using Project;
using UnityEngine;

namespace Gameplay {

    public class ConstructState : State {
        private readonly ContentCreationService _contentCreationService;
        private readonly GameCache _gameCache;
        private readonly BotNavigation _botNavigation;
        private readonly EnemyPointerSystem _enemyPointerSystem;
        private readonly StagesConfig _stagesConfig;
        private readonly GameProcess _gameProcess;
        private readonly CameraSystem _cameraSystem;
        private readonly StateSwitcher _stateSwitcher;
        private readonly SaveLoadSystem _saveLoadSystem;
        private readonly VehicleController _vehicleController;
        private readonly VehicleDestroyer _vehicleDestroyer;
        private readonly StageLabelPresenter _stageLabelPresenter;

        public ConstructState(
            StateSwitcher stateSwitcher,
            ContentCreationService contentCreationService,
            CameraSystem cameraSystem,
            GameProcess gameProcess,
            SaveLoadSystem saveLoadSystem,
            VehicleController vehicleController,
            VehicleDestroyer vehicleDestroyer,
            StageLabelPresenter stageLabelPresenter,
            GameCache gameCache,
            BotNavigation botNavigation,
            EnemyPointerSystem enemyPointerSystem,
            StagesConfig stagesConfig) : base(stateSwitcher) {
            _stateSwitcher = stateSwitcher;
            _contentCreationService = contentCreationService;
            _cameraSystem = cameraSystem;
            _gameProcess = gameProcess;
            _saveLoadSystem = saveLoadSystem;
            _vehicleController = vehicleController;
            _vehicleDestroyer = vehicleDestroyer;
            _stageLabelPresenter = stageLabelPresenter;
            _gameCache = gameCache;
            _botNavigation = botNavigation;
            _enemyPointerSystem = enemyPointerSystem;
            _stagesConfig = stagesConfig;
        }

        public override void Enter() {
            LoadFromCloudToCache();
            CreateGameplayObjects();
            SnapCameraToCar();
            InitializeGameplay();

            var mapsCount = _gameCache.mapsCount;
            var mapIndex = _gameCache.mapIndex;
            SetStageNumber(_gameCache.stageIndex, mapIndex, mapsCount);

            if (mapIndex == 0)
                SwitchToMenuState();
            else
                SwitchToHowToPlayState();
        }

        private void CreateGameplayObjects() {
            var stageIndex = _gameCache.stageIndex;
            var currentCarIndex = _gameCache.currentCarIndex;
            var mapIndex = _gameCache.mapIndex;
            var map = _contentCreationService.CreateMap(stageIndex, mapIndex);
            map.navMeshSurface.BuildNavMesh();
            _gameCache.map = map;
            _gameCache.car = _contentCreationService.CreateCar(currentCarIndex, _gameCache.map.startPoint);
            _gameCache.zombies = _contentCreationService.CreateZombies(_gameCache.map.zombieSpawnPoints);
        }

        private void LoadFromCloudToCache() {
            var saveData = _saveLoadSystem.Load();
            var stageIndex = saveData.stageIndex;
            _gameCache.saveData1 = saveData;
            _gameCache.mapsCount = _stagesConfig.stages[stageIndex].count;
        }

        private void SetStageNumber(int stageIndex, int mapIndex, int mapsCount) {
            _stageLabelPresenter.stageIndex = stageIndex;
            _stageLabelPresenter.mapIndex = new Vector2Int(mapIndex, mapsCount);
        }

        private void SnapCameraToCar() {
            _cameraSystem.target = _gameCache.car.transform;
        }

        private void InitializeGameplay() {
            var car = _gameCache.car;
            car.Initialize();

            _vehicleController.SetCar(car);
            _vehicleDestroyer.SetCar(car);

            var zombiesArray = _gameCache.zombies;
            _botNavigation.Initialize(zombiesArray, car.transform);
            _gameProcess.Initialize(car, zombiesArray);
            _enemyPointerSystem.SetNewData(zombiesArray, car.transform);
        }

        private void SwitchToMenuState() {
            _stateSwitcher.SetState<MenuState>();
        }

        private void SwitchToHowToPlayState() {
            _stateSwitcher.SetState<GetReadyState>();
        }
    }
}