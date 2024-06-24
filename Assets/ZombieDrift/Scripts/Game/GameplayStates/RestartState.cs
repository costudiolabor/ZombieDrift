public class RestartState : State {
    private readonly StateSwitcher _stateSwitcher;
    private readonly PlayCache _playCache;

    public RestartState(StateSwitcher stateSwitcher, PlayCache playCache) : base(stateSwitcher) {
        _stateSwitcher = stateSwitcher;
        _playCache = playCache;
    }

    public override void Enter() {
        var car = _playCache.gameplayData.car;
        var startPose =_playCache.gameplayData.map.startPoint;

        car.transform.position = startPose.position; 
        car.transform.rotation = startPose.rotation; 
        SwitchToGameplayState();
    }

    private void SwitchToGameplayState() {
        _stateSwitcher.SetState<PlayState>();
    }
}