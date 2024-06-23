public class GameScenario {
	
	private readonly StateSwitcher _stateSwitcher;

	public GameScenario(StateSwitcher stateSwitcher, PrepareMapState prepareMapState ) {
		_stateSwitcher = stateSwitcher;
		_stateSwitcher.AddState(prepareMapState);
	}
	
	public void Start() {
	 	_stateSwitcher.SetState<PrepareMapState>();
	}
}