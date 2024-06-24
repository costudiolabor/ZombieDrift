public class LoseState : State {
    private readonly StateSwitcher _stateSwitcher;
    private readonly ProgressSystem _progressSystem;
    private readonly PlayCache _playCache;

    public LoseState(StateSwitcher stateSwitcher,
        ProgressSystem progressSystem,
        PlayCache playCache) : base(stateSwitcher) {
        _stateSwitcher = stateSwitcher;
        _progressSystem = progressSystem;
        _playCache = playCache;
    }

    public override void Enter() {
        SwitchToRestartState();
   //     SwitchToPrepareState();
    }

    private void SwitchToRestartState() {
        _stateSwitcher.SetState<RestartState>();
    }

    private void SwitchToPrepareState() {
        _stateSwitcher.SetState<ConstructState>();
    }
}