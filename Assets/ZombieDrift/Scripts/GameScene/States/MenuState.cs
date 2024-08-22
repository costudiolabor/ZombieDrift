using Project;
using UnityEngine;

namespace Gameplay {

    public class MenuState : State {
        private readonly StateSwitcher _stateSwitcher;
        private readonly MainMenuPresenter _mainMenuPresenter;
        private readonly StageLabelPresenter _stageLabelPresenter;
        private readonly ScenesLoader _scenesLoader;

        public MenuState(
            StateSwitcher stateSwitcher,
            MainMenuPresenter mainMenuPresenter,
            StageLabelPresenter stageLabelPresenter,
            ScenesLoader scenesLoader) : base(stateSwitcher) {
            _stateSwitcher = stateSwitcher;
            _mainMenuPresenter = mainMenuPresenter;
            _stageLabelPresenter = stageLabelPresenter;
            _scenesLoader = scenesLoader;
        }

        public override void Enter() {
            _stageLabelPresenter.presentState = StagePresentState.StageOnly;
            _mainMenuPresenter.enabled = true;
            _mainMenuPresenter.StartGameEvent += SwitchToPlayState;
            _mainMenuPresenter.GarageEvent += SwitchToGarageState;
        }

        public override void Exit() {
            _stageLabelPresenter.presentState = StagePresentState.None;
            _mainMenuPresenter.enabled = false;
            _mainMenuPresenter.StartGameEvent -= SwitchToPlayState;
            _mainMenuPresenter.GarageEvent -= SwitchToGarageState;
        }

        private void SwitchToGarageState() =>
            _scenesLoader.SwitchToShopScene();

        private void SwitchToPlayState() {
            _stateSwitcher.SetState<GetReadyState>();
        }
    }
}