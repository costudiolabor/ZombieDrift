using UnityEngine;

public class PrepareState : State {
    private readonly ContentCreationService _contentCreationService;
    private readonly StateSwitcher _stateSwitcher;
    private readonly CameraSystem _cameraSystem;
    private readonly GameProcess _gameProcess;
    private readonly VehicleController _vehicleController;
    private readonly CacheSystem _cacheSystem;

    public PrepareState(
        StateSwitcher stateSwitcher,
        ContentCreationService contentCreationService,
        CameraSystem cameraSystem,
        GameProcess gameProcess,
        VehicleController vehicleController,
        CacheSystem cacheSystem) : base(stateSwitcher) {
        _stateSwitcher = stateSwitcher;
        _contentCreationService = contentCreationService;
        _cameraSystem = cameraSystem;
        _gameProcess = gameProcess;
        _vehicleController = vehicleController;
        _cacheSystem = cacheSystem;
    }

    public override void Enter() {
        var stageIndex = _progressSystem.stageIndex;
        var maps = _stagesConfig.stages[stageIndex].maps;
        _cacheSystem.mapPrefabs = maps.ToList();
        var mapPrefab = _cacheSystem.GetNextMap();
        var carPrefab = _cacheSystem.carPrefab;
        var carIndex = _progressSystem.currentCarIndex;
        var car = _carsConfig.cars[carIndex];
   

        var gameplayData = CreateGameContent(mapPrefab, carPrefab);
        _cacheSystem.gameplayData = gameplayData;

        SnapGameCamera(gameplayData.car.transform);
        InitializeGameplay(gameplayData);
        SwitchToGameplayState();
    }

    private GameplayData CreateGameContent(Map mapPrefab, Car carPrefab) {
        var map = _contentCreationService.CreateMap(mapPrefab);
        var zombies = _contentCreationService.CreateZombies(map.zombieSpawnPoints);
        var car = _contentCreationService.CreateCar(carPrefab, map.startPoint);

        return new GameplayData(map, zombies, car);
    }
    
    private void SnapGameCamera(Transform target) {
        _cameraSystem.target = target.transform;
    }

    private void InitializeGameplay(GameplayData gameplayData) {
        var car = gameplayData.car;
        car.Initialize();

        _vehicleController.Initialize(car);
        _gameProcess.Initialize(gameplayData);
    }

    private void SwitchToGameplayState() {
        _stateSwitcher.SetState<PlayState>();
    }
}