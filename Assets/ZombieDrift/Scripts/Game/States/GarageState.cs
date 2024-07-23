public class GarageState : State {
    private readonly StateSwitcher _stateSwitcher;
    private readonly SaveLoadSystem _saveLoadSystem;
    private readonly GameCache _gameCache;
    private readonly StagesConfig _stagesConfig;

    public GarageState(StateSwitcher stateSwitcher,
        SaveLoadSystem saveLoadSystem,
        GameCache gameCache,
        StagesConfig stagesConfig) : base(stateSwitcher) {
        _stateSwitcher = stateSwitcher;
        _saveLoadSystem = saveLoadSystem;
        _gameCache = gameCache;
        _stagesConfig = stagesConfig;
    }

    public override void Enter() {
  
    }



    private void SwitchToPrepareState() {
        _stateSwitcher.SetState<FinalizeState>();
    }
}