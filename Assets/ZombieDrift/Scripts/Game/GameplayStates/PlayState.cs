using UnityEngine;

public class PlayState : State {
    private readonly StateSwitcher _stateSwitcher;
    private readonly GameProcess _gameProcess;
    private readonly PauseService _pauseService;
    private readonly VehicleController _vehicleController;

    public PlayState(StateSwitcher stateSwitcher,
        GameProcess gameProcess,
        PauseService pauseService,
        VehicleController vehicleController) : base(stateSwitcher) {
        _stateSwitcher = stateSwitcher;
        _gameProcess = gameProcess;
        _pauseService = pauseService;
        _vehicleController = vehicleController;
    }

    public override void Enter() {
        _pauseService.SetPause(false);
        
        _vehicleController.Start();
        _gameProcess.ObstacleHitEvent += SwitchToLoseState;
        _gameProcess.AllEnemiesDestroyedEvent += SwitchToWinState;
    }

    public override void Exit() {
        _pauseService.SetPause(true);
        
        _vehicleController.Stop();
        _gameProcess.ObstacleHitEvent -= SwitchToLoseState;
        _gameProcess.AllEnemiesDestroyedEvent -= SwitchToWinState;
    }

    private void SwitchToWinState() {
       _stateSwitcher.SetState<WinState>();
    }

    private void SwitchToLoseState(Vector3 obj) {
        _stateSwitcher.SetState<LoseState>();
    }
}