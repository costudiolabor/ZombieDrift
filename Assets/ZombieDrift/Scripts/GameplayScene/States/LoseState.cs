using Project;

namespace Gameplay {
	public class LoseState : State {
		private readonly StateSwitcher _stateSwitcher;
		private readonly SaveLoadSystem _saveLoadSystem;
		private readonly GameplayCache _gameplayCache;
		private readonly LosePresenter _losePresenter;
		private readonly CameraSystem _cameraSystem;

		public LoseState(
				StateSwitcher stateSwitcher,
				SaveLoadSystem saveLoadSystem,
				GameplayCache gameplayCache,
				LosePresenter losePresenter,
				CameraSystem cameraSystem) : base(stateSwitcher) {
			_stateSwitcher = stateSwitcher;
			_saveLoadSystem = saveLoadSystem;
			_gameplayCache = gameplayCache;
			_losePresenter = losePresenter;
			_cameraSystem = cameraSystem;
		}

		public override void Enter() {
			CameraActions();

			_losePresenter.enabled = true;
			_losePresenter.RepairEvent += SwitchToPrepareState;
			_losePresenter.RestartEvent += SwitchToRestartState;
		}

		private async void CameraActions() {
			await _cameraSystem.Shake(1, 250);
			_cameraSystem.isZoomed = true;
		}

		public override void Exit() {
			_losePresenter.enabled = false;
			_losePresenter.RepairEvent -= SwitchToRestartState;
			_losePresenter.RestartEvent -= SwitchToPrepareState;
			_cameraSystem.isZoomed = false;
		}

		private void SwitchToRestartState() {
			_gameplayCache.mapIndex = 0;
			_stateSwitcher.SetState<FinalizeState>();
		}

		private void SwitchToPrepareState() {
			_stateSwitcher.SetState<RepairState>();
		}
	}
}
