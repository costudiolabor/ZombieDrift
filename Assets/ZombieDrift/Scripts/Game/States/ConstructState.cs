using System.Linq;
using UnityEngine;

public class ConstructState : State {
    private readonly ContentCreationService _contentCreationService;
    private readonly GameCache _gameCache;
    private readonly BotNavigation _botNavigation;
    private readonly StagesConfig _stagesConfig;
    private readonly GameProcess _gameProcess;
    private readonly CameraSystem _cameraSystem;
    private readonly StateSwitcher _stateSwitcher;
    private readonly SaveLoadSystem _saveLoadSystem;
    private readonly VehicleController _vehicleController;
    private readonly VehicleDestroyer _vehicleDestroyer;
    private readonly StagePresenter _stagePresenter;

    public ConstructState(
        StateSwitcher stateSwitcher,
        ContentCreationService contentCreationService,
        CameraSystem cameraSystem,
        GameProcess gameProcess,
        SaveLoadSystem saveLoadSystem,
        VehicleController vehicleController,
        VehicleDestroyer vehicleDestroyer,
        StagePresenter stagePresenter,
        GameCache gameCache,
        BotNavigation botNavigation,
        StagesConfig stagesConfig) : base(stateSwitcher) {
        _stateSwitcher = stateSwitcher;
        _contentCreationService = contentCreationService;
        _cameraSystem = cameraSystem;
        _gameProcess = gameProcess;
        _saveLoadSystem = saveLoadSystem;
        _vehicleController = vehicleController;
        _vehicleDestroyer = vehicleDestroyer;
        _stagePresenter = stagePresenter;
        _gameCache = gameCache;
        _botNavigation = botNavigation;
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
        _stagePresenter.stageIndex = stageIndex; 
        _stagePresenter.mapIndex = new Vector2Int(mapIndex, mapsCount);
    }
    
    private void SnapCameraToCar() {
        _cameraSystem.target = _gameCache.car.transform;
    }

    private void InitializeGameplay() {
        var car = _gameCache.car;
        car.Initialize();

        _vehicleController.SetCar(car);
        _vehicleDestroyer.SetCar(car);
        _botNavigation.Initialize(_gameCache.zombies,car.transform );

        _gameProcess.Initialize(car, _gameCache.zombies);
    }

    private void SwitchToMenuState() {
        _stateSwitcher.SetState<MenuState>();
    }
    private void SwitchToHowToPlayState() {
        _stateSwitcher.SetState<GetReadyState>();
    }
}