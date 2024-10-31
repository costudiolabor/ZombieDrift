using Project;
using UnityEngine;
using SaveLoadSystemNamespace;

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
			Debug.Log("Lose");

			CameraActions();

			_losePresenter.enabled = true;
			_losePresenter.RepairEvent += SwitchToRepairState;
			_losePresenter.RestartEvent += SwitchToRestartState;
		}
		
		public override void Exit() {
			_losePresenter.enabled = false;
			_losePresenter.RepairEvent -= SwitchToRepairState;
			_losePresenter.RestartEvent -= SwitchToRestartState;
			_cameraSystem.isZoomed = false;
		}
		
		private async void CameraActions() {
			await _cameraSystem.Shake(1, 250);
			_cameraSystem.isZoomed = true;
		}
		private void SwitchToRestartState() {
			_gameplayCache.mapIndex = 0;
			_stateSwitcher.SetState<FinalizeState>();
		}

		private void SwitchToRepairState() {
			_stateSwitcher.SetState<RepairState>();
		}
	}
}
