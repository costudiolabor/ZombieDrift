public class WinState : State {
    private readonly StateSwitcher _stateSwitcher;
    private readonly ProgressSystem _progressSystem;
    private readonly PlayCache _playCache;
    private readonly StagesConfig _stagesConfig;

    public WinState(StateSwitcher stateSwitcher,
        ProgressSystem progressSystem,
        PlayCache playCache,
        StagesConfig stagesConfig) : base(stateSwitcher) {
        _stateSwitcher = stateSwitcher;
        _progressSystem = progressSystem;
        _playCache = playCache;
        _stagesConfig = stagesConfig;
    }

    public override void Enter() {
        //Показать заставкку Win
        IncreaseMapIndex();
        SwitchToPrepareState();
    }

    private void IncreaseMapIndex() {
        var stageIndex = _progressSystem.stageIndex;
        var mapIndex = _progressSystem.mapIndex + 1;
            
        var mapsCount = _stagesConfig.stages[stageIndex].count;
        if (mapIndex > mapsCount - 1) {
            _progressSystem.IncreaseStageIndex();
            _progressSystem.mapIndex = 0;
        }
        else {
            _progressSystem.mapIndex = mapIndex;
        }
    }

    private void SwitchToPrepareState() {
        _stateSwitcher.SetState<FinalizeState>();
    }
}