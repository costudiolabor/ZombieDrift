using Ads;
using Project;
using UnityEngine;

namespace Gameplay {
	public class MenuState : State {
		private readonly StateSwitcher _stateSwitcher;
		private readonly MenuPresenter _menuPresenter;
		private readonly GameplayHudPresenter _gameplayHudPresenter;
		private readonly ScenesLoader _scenesLoader;
		private readonly AdsSystem _adsSystem;

		public MenuState(
				StateSwitcher stateSwitcher,
				MenuPresenter menuPresenter,
				GameplayHudPresenter gameplayHudPresenter,
				ScenesLoader scenesLoader,
				AdsSystem adsSystem
		) : base(stateSwitcher) {
			_stateSwitcher = stateSwitcher;
			_menuPresenter = menuPresenter;
			_gameplayHudPresenter = gameplayHudPresenter;
			_scenesLoader = scenesLoader;
			_adsSystem = adsSystem;
		}

		public override void Enter() {
		_gameplayHudPresenter.presentState = StagePresentState.StageOnly;
			_menuPresenter.enabled = true;
			_menuPresenter.StartGameEvent += SwitchToPlayState;
			_menuPresenter.GarageEvent += SwitchToGarageState;
		}

		public override void Exit() {
			_gameplayHudPresenter.presentState = StagePresentState.None;
			_menuPresenter.enabled = false;
			_menuPresenter.StartGameEvent -= SwitchToPlayState;
			_menuPresenter.GarageEvent -= SwitchToGarageState;
		}

		private void SwitchToGarageState() {
			_adsSystem.ShowFullscreen();
			_scenesLoader.SwitchToShopScene();
		}

		private void SwitchToPlayState() =>
				_stateSwitcher.SetState<GetReadyState>();
	}
}
