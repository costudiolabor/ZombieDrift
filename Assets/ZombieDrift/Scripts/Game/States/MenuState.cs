using UnityEngine;

public class MenuState : State {
    private readonly StateSwitcher _stateSwitcher;
    private readonly MainMenuPresenter _mainMenuPresenter;
    private readonly StagePresenter _stagePresenter;

    public MenuState(
        StateSwitcher stateSwitcher,
        MainMenuPresenter mainMenuPresenter,
        StagePresenter stagePresenter) : base(stateSwitcher) {
        _stateSwitcher = stateSwitcher;
        _mainMenuPresenter = mainMenuPresenter;
        _stagePresenter = stagePresenter;
    }

    public override void Enter() {
        _stagePresenter.presentState = StagePresentState.StageOnly;
        _mainMenuPresenter.enabled = true;
        _mainMenuPresenter.StartGameEvent += SwitchToPlayState;
        _mainMenuPresenter.GarageEvent += SwitchToGarageState;
    }

    public override void Exit() {
        _stagePresenter.presentState = StagePresentState.None;
        _mainMenuPresenter.enabled = false;
        _mainMenuPresenter.StartGameEvent -= SwitchToPlayState;
        _mainMenuPresenter.GarageEvent -= SwitchToGarageState;
    }

    private void SwitchToGarageState() {
        _stateSwitcher.SetState<GarageState>();
    }

    private void SwitchToPlayState() {
        _stateSwitcher.SetState<GetReadyState>();
    }
}