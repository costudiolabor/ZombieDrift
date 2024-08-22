using Gameplay;
using UnityEngine;

public class PlayState : State {
    private const float SHAKE_AMPLITUDE = 0.5f;
    private const int SHAKE_DURATION = 85;

    private readonly StateSwitcher _stateSwitcher;
    private readonly StageLabelPresenter _stageLabelPresenter;
    private readonly GameProcess _gameProcess;
    private readonly PauseService _pauseService;
    private readonly VehicleController _vehicleController;
    private readonly VehicleDestroyer _vehicleDestroyer;
    private readonly CameraSystem _cameraSystem;
    private readonly BotNavigation _botNavigation;
    private readonly ParticlesPlayer _particlesPlayer;
    private readonly EnemyPointerSystem _enemyPointerSystem;

    public PlayState(StateSwitcher stateSwitcher,
        StageLabelPresenter stageLabelPresenter,
        GameProcess gameProcess,
        PauseService pauseService,
        VehicleController vehicleController,
        VehicleDestroyer vehicleDestroyer,
        CameraSystem cameraSystem,
        BotNavigation botNavigation,
        ParticlesPlayer particlesPlayer,
        EnemyPointerSystem enemyPointerSystem) : base(stateSwitcher) {
        _stateSwitcher = stateSwitcher;
        _stageLabelPresenter = stageLabelPresenter;
        _gameProcess = gameProcess;
        _pauseService = pauseService;
        _vehicleController = vehicleController;
        _vehicleDestroyer = vehicleDestroyer;
        _cameraSystem = cameraSystem;
        _botNavigation = botNavigation;
        _particlesPlayer = particlesPlayer;
        _enemyPointerSystem = enemyPointerSystem;
    }

    public override void Enter() {
        _stageLabelPresenter.presentState = StagePresentState.All;
        _pauseService.SetPause(false);

        _vehicleController.Start();
        _botNavigation.Start();
        _enemyPointerSystem.enabled = true;

        _gameProcess.ObstacleHitEvent += OnCarHitObstacle;
        _gameProcess.AllEnemiesDestroyedEvent += SwitchToWinState;
        _gameProcess.ZombieHitEvent += OnEnemyHit;
    }

    public override void Exit() {
        _stageLabelPresenter.presentState = StagePresentState.None;
        _pauseService.SetPause(true);

        _vehicleController.Stop();
        _botNavigation.Stop();
        _enemyPointerSystem.enabled = false;
        
        _gameProcess.ObstacleHitEvent -= OnCarHitObstacle;
        _gameProcess.AllEnemiesDestroyedEvent -= SwitchToWinState;
        _gameProcess.ZombieHitEvent -= OnEnemyHit;
    }

    public override void Tick() {
        _botNavigation.Tick();
        _enemyPointerSystem.Tick();
    }

    private async void OnEnemyHit(Zombie zombie) {
        _particlesPlayer.PlayZombieHit(zombie.position);
        _botNavigation.RemoveKilledZombie(zombie);
        _enemyPointerSystem.Remove(zombie);
        await _cameraSystem.Shake(SHAKE_AMPLITUDE, SHAKE_DURATION);
    }

    private void OnCarHitObstacle(Vector3 point) {
        _particlesPlayer.PlayObstacleHit(point);
        _vehicleDestroyer.DestroyFormPoint(point);
        SwitchToLoseState();
    }

    private void SwitchToWinState() =>
        _stateSwitcher.SetState<WinState>();
    
    private void SwitchToLoseState() =>
        _stateSwitcher.SetState<LoseState>();
}