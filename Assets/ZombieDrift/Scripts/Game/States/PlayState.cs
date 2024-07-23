using UnityEngine;

public class PlayState : State {
    private const float SHAKE_AMPLITUDE = 0.5f;
    private const int SHAKE_DURATION = 85;

    private readonly StateSwitcher _stateSwitcher;
    private readonly StagePresenter _stagePresenter;
    private readonly GameProcess _gameProcess;
    private readonly PauseService _pauseService;
    private readonly VehicleController _vehicleController;
    private readonly VehicleDestroyer _vehicleDestroyer;
    private readonly CameraSystem _cameraSystem;
    private readonly BotNavigation _botNavigation;

    public PlayState(StateSwitcher stateSwitcher,
        StagePresenter stagePresenter,
        GameProcess gameProcess,
        PauseService pauseService,
        VehicleController vehicleController,
        VehicleDestroyer vehicleDestroyer,
        CameraSystem cameraSystem,
        BotNavigation botNavigation) : base(stateSwitcher) {
        _stateSwitcher = stateSwitcher;
        _stagePresenter = stagePresenter;
        _gameProcess = gameProcess;
        _pauseService = pauseService;
        _vehicleController = vehicleController;
        _vehicleDestroyer = vehicleDestroyer;
        _cameraSystem = cameraSystem;
        _botNavigation = botNavigation;
    }

    public override void Enter() {
        _stagePresenter.presentState = StagePresentState.All;
        _pauseService.SetPause(false);

        _vehicleController.Start();
        _botNavigation.Start();
        _gameProcess.ObstacleHitEvent += SwitchToLoseState;
        _gameProcess.AllEnemiesDestroyedEvent += SwitchToWinState;
        _gameProcess.ZombieHitEvent += OnEnemyHit;
    }

    public override void Exit() {
        _stagePresenter.presentState = StagePresentState.None;
        _pauseService.SetPause(true);

        _vehicleController.Stop();
        _botNavigation.Stop();
        _gameProcess.ObstacleHitEvent -= SwitchToLoseState;
        _gameProcess.AllEnemiesDestroyedEvent -= SwitchToWinState;
        _gameProcess.ZombieHitEvent -= OnEnemyHit;
    }

    private async void OnEnemyHit(Zombie obj) {
        _botNavigation.RemoveKilledZombie(obj);
        await _cameraSystem.Shake(SHAKE_AMPLITUDE, SHAKE_DURATION);
    }

    private void SwitchToWinState() {
        _stateSwitcher.SetState<WinState>();
    }

    private void SwitchToLoseState(Vector3 obj) {
        _vehicleDestroyer.Destroy(obj);

        _stateSwitcher.SetState<LoseState>();
    }
}