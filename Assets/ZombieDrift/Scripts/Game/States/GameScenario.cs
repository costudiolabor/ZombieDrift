public class GameScenario {
	
	private readonly StateSwitcher _stateSwitcher;

	public GameScenario(
		StateSwitcher stateSwitcher,
		ConstructState constructState,
		MenuState menuState,
		GetReadyState getReadyState,
		PlayState playState,
		WinState winState,
		LoseState loseState,
		RepairState repairState,
		FinalizeState finalizeState) {
		_stateSwitcher = stateSwitcher;
		_stateSwitcher.AddState(constructState);
		_stateSwitcher.AddState(menuState);
		_stateSwitcher.AddState(getReadyState);
		_stateSwitcher.AddState(playState);
		_stateSwitcher.AddState(winState);
		_stateSwitcher.AddState(loseState);
		_stateSwitcher.AddState(repairState);
		_stateSwitcher.AddState(finalizeState);
	}
	
	public void Start() {
	 	_stateSwitcher.SetState<ConstructState>();
	}
}