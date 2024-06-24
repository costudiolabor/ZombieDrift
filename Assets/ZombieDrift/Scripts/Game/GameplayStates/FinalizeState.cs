public class FinalizeState : State {
    private readonly StateSwitcher _stateSwitcher;
    private readonly PlayCache _playCache;
    private readonly GameProcess _gameProcess;

    public FinalizeState(StateSwitcher stateSwitcher,
        PlayCache playCache,
        GameProcess gameProcess) : base(stateSwitcher) {
        _stateSwitcher = stateSwitcher;
        _playCache = playCache;
        _gameProcess = gameProcess;
    }

    public override void Enter() {
        _gameProcess.Finish();
        _playCache.DestroyGameObjectsAndClear();

        SwitchToPrepare();
    }

    private void SwitchToPrepare() {
        _stateSwitcher.SetState<ConstructState>();
    }
}