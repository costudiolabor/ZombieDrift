using Project;

namespace Gameplay {
	public class MenuState : State {
		private readonly StateSwitcher _stateSwitcher;
		private readonly MenuPresenter _menuPresenter;
		private readonly GameplayHud _gameplayHud;
		private readonly ScenesLoader _scenesLoader;

		public MenuState(
				StateSwitcher stateSwitcher,
				MenuPresenter menuPresenter,
				GameplayHud gameplayHud,
				ScenesLoader scenesLoader) : base(stateSwitcher) {
			_stateSwitcher = stateSwitcher;
			_menuPresenter = menuPresenter;
			_gameplayHud = gameplayHud;
			_scenesLoader = scenesLoader;
		}

		public override void Enter() {
			_gameplayHud.presentState = StagePresentState.StageOnly;
			_menuPresenter.enabled = true;
			_menuPresenter.StartGameEvent += SwitchToPlayState;
			_menuPresenter.GarageEvent += SwitchToGarageState;
		}

		public override void Exit() {
			_gameplayHud.presentState = StagePresentState.None;
			_menuPresenter.enabled = false;
			_menuPresenter.StartGameEvent -= SwitchToPlayState;
			_menuPresenter.GarageEvent -= SwitchToGarageState;
		}

		private void SwitchToGarageState() =>
				_scenesLoader.SwitchToShopScene();

		private void SwitchToPlayState() =>
				_stateSwitcher.SetState<GetReadyState>();
	}
}