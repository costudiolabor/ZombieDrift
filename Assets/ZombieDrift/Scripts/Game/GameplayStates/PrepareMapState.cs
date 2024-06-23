
public class PrepareMapState : State
{
    private readonly ContentCreationService _contentCreationService;
    private readonly ProgressService _progressService;
    private readonly CameraSystem _cameraSystem;

    public PrepareMapState(
        StateSwitcher stateSwitcher,
        ContentCreationService contentCreationService,
        ProgressService progressService,
        CameraSystem cameraSystem) : base(stateSwitcher) {
        _contentCreationService = contentCreationService;
        _progressService = progressService;
        _cameraSystem = cameraSystem;
    }

    public override void Enter() {
        var currentStageIndex = _progressService.stageIndex;
        var carIndex = _progressService.currentCarIndex;
       
        var map = _contentCreationService.CreateMap(currentStageIndex, 0);
        var zombies =  _contentCreationService.CreateZombies(map.zombieSpawnPoints);
        var car = _contentCreationService.CreateCar(carIndex, map.startPoint);

        var gameplayData = new GameplayData(map, zombies, car);
        _cameraSystem.target = car.transform;
    }
    
}
