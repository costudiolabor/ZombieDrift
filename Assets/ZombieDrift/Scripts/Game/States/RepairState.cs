using UnityEngine;

public class RepairState : State {
    private readonly StateSwitcher _stateSwitcher;
    private readonly GameCache _gameCache;
    private readonly VehicleDestroyer _vehicleDestroyer;
    private readonly VehicleController _vehicleController;
    private readonly ContentCreationService _contentCreationService;
    private readonly CameraSystem _cameraSystem;
    private readonly GameProcess _gameProcess;
    private readonly BotNavigation _botNavigation;

    public RepairState(
        StateSwitcher stateSwitcher,
        GameCache gameCache,
        VehicleDestroyer vehicleDestroyer,
        VehicleController vehicleController,
        ContentCreationService contentCreationService,
        CameraSystem cameraSystem,
        GameProcess gameProcess,
        BotNavigation botNavigation) : base(stateSwitcher) {
        _stateSwitcher = stateSwitcher;
        _gameCache = gameCache;
        _vehicleDestroyer = vehicleDestroyer;
        _vehicleController = vehicleController;
        _contentCreationService = contentCreationService;
        _cameraSystem = cameraSystem;
        _gameProcess = gameProcess;
        _botNavigation = botNavigation;
    }

    public override void Enter() {
        DestroyCarObject();
        CreateAndInitializeNewCar();
        SwitchToGameplayState();
    }

    private void DestroyCarObject() {
        Object.Destroy(_gameCache.car.gameObject);
        _gameCache.car = null;
    }

    private void CreateAndInitializeNewCar() {
        var car = _contentCreationService.CreateCar(_gameCache.currentCarIndex, _gameCache.map.startPoint);
        var startPose = _gameCache.map.startPoint;
        car.transform.position = startPose.position;
        car.transform.rotation = startPose.rotation;
        car.Initialize();

        _cameraSystem.target = car.transform;
        _vehicleController.SetCar(car);
        _vehicleDestroyer.SetCar(car);
        _botNavigation.target = car.transform;
        _gameProcess.SetCar(car);
        _gameCache.car = car;
    }

    private void SwitchToGameplayState() {
        _stateSwitcher.SetState<GetReadyState>();
    }
}