using UnityEngine;

public class ConstructState : State {
    private readonly ContentCreationService _contentCreationService;
    private readonly PlayCache _gamePlayCache;
    private readonly GameProcess _gameProcess;
    private readonly CameraSystem _cameraSystem;
    private readonly StateSwitcher _stateSwitcher;
    private readonly ProgressSystem _progressSystem;
    private readonly VehicleController _vehicleController;

    public ConstructState(
        StateSwitcher stateSwitcher,
        ContentCreationService contentCreationService,
        CameraSystem cameraSystem,
        GameProcess gameProcess,
        ProgressSystem progressSystem,
        VehicleController vehicleController,
        PlayCache gamePlayCache) : base(stateSwitcher) {
        _stateSwitcher = stateSwitcher;
        _contentCreationService = contentCreationService;
        _cameraSystem = cameraSystem;
        _gameProcess = gameProcess;
        _progressSystem = progressSystem;
        _vehicleController = vehicleController;
        _gamePlayCache = gamePlayCache;
    }

    public override void Enter() {
        var gameplayData = CreateGameContent();
        _gamePlayCache.gameplayData = gameplayData;

        SnapGameCamera(gameplayData.car.transform);
        InitializeGameplay(gameplayData);
        SwitchToGameplayState();
    }

    private GameplayData CreateGameContent() {
        var stageIndex = _progressSystem.stageIndex;
        var mapIndex = _progressSystem.mapIndex;
       
        var carIndex = _progressSystem.currentCarIndex;
       
        var map = _contentCreationService.CreateMap(stageIndex, mapIndex);
        var zombies = _contentCreationService.CreateZombies(map.zombieSpawnPoints);
        var car = _contentCreationService.CreateCar(carIndex, map.startPoint);

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