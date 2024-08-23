using Project;
using UnityEngine;

namespace Gameplay {

    public class ConstructState : State {
        private readonly ContentCreationService _contentCreationService;
        private readonly ProjectCache _projectCache;
        private readonly GameplayCache _gameplayCache;
        private readonly BotNavigation _botNavigation;
        private readonly EnemyPointerSystem _enemyPointerSystem;
        private readonly StagesConfig _stagesConfig;
        private readonly GameProcess _gameProcess;
        private readonly CameraSystem _cameraSystem;
        private readonly StateSwitcher _stateSwitcher;
        private readonly SaveLoadSystem _saveLoadSystem;
        private readonly VehicleController _vehicleController;
        private readonly VehicleDestroyer _vehicleDestroyer;
        private readonly StageLabel _stageLabel;

        public ConstructState(
            StateSwitcher stateSwitcher,
            ContentCreationService contentCreationService,
            CameraSystem cameraSystem,
            GameProcess gameProcess,
            SaveLoadSystem saveLoadSystem,
            VehicleController vehicleController,
            VehicleDestroyer vehicleDestroyer,
            StageLabel stageLabel,
            BotNavigation botNavigation,
            EnemyPointerSystem enemyPointerSystem,
            StagesConfig stagesConfig,
            ProjectCache projectCache,
            GameplayCache gameplayCache) : base(stateSwitcher) {
            _stateSwitcher = stateSwitcher;
            _contentCreationService = contentCreationService;
            _cameraSystem = cameraSystem;
            _gameProcess = gameProcess;
            _saveLoadSystem = saveLoadSystem;
            _vehicleController = vehicleController;
            _vehicleDestroyer = vehicleDestroyer;
            _stageLabel = stageLabel;
            _projectCache = projectCache;
            _gameplayCache = gameplayCache;
            _botNavigation = botNavigation;
            _enemyPointerSystem = enemyPointerSystem;
            _stagesConfig = stagesConfig;
        }

        public override void Enter() {
            LoadGameplayCache();
            CreateGameplayObjects();
            SnapCameraToCar();
            InitializeGameplay();

            var mapsCount = _gameplayCache.mapsCount;
            var mapIndex = _gameplayCache.mapIndex;
            SetStageNumber(_projectCache.stageIndex, mapIndex, mapsCount);

            if (mapIndex == 0)
                SwitchToMenuState();
            else
                SwitchToHowToPlayState();
        }

        private void CreateGameplayObjects() {
            var stageIndex = _projectCache.stageIndex;
            var currentCarIndex = _projectCache.selectedCarIndex;
            var mapIndex = _gameplayCache.mapIndex;
            var map = _contentCreationService.CreateMap(stageIndex, mapIndex);
            map.navMeshSurface.BuildNavMesh();
            _gameplayCache.map = map;
            _gameplayCache.car = _contentCreationService.CreateCar(currentCarIndex, map.startPoint);
            _gameplayCache.zombies = _contentCreationService.CreateZombies(map.zombieSpawnPoints);
        }

         private void LoadGameplayCache() {
            var stageIndex = _projectCache.stageIndex;
            _gameplayCache.mapsCount = _stagesConfig.stages[stageIndex].count;
        }

        private void SetStageNumber(int stageIndex, int mapIndex, int mapsCount) {
            _stageLabel.stageIndex = stageIndex;
            _stageLabel.mapIndex = new Vector2Int(mapIndex, mapsCount);
        }

        private void SnapCameraToCar() {
            _cameraSystem.target = _gameplayCache.car.transform;
        }

        private void InitializeGameplay() {
            var car = _gameplayCache.car;
            car.Initialize();

            _vehicleController.SetCar(car);
            _vehicleDestroyer.SetCar(car);

            var zombiesArray = _gameplayCache.zombies;
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