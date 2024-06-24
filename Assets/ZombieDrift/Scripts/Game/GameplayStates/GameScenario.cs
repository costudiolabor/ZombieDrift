public class GameScenario {
	
	private readonly StateSwitcher _stateSwitcher;

	public GameScenario(
		StateSwitcher stateSwitcher,
		ConstructState constructState,
		PlayState playState,
		WinState winState,
		LoseState loseState,
		RestartState restartState,
		FinalizeState finalizeState) {
		_stateSwitcher = stateSwitcher;
		_stateSwitcher.AddState(constructState);
		_stateSwitcher.AddState(playState);
		_stateSwitcher.AddState(winState);
		_stateSwitcher.AddState(loseState);
		_stateSwitcher.AddState(restartState);
		_stateSwitcher.AddState(finalizeState);
	}
	
	public void Start() {
	 	_stateSwitcher.SetState<ConstructState>();
	}
}