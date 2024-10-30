using Project;
using UnityEngine;

namespace Gameplay {
	public class PauseState : State {
		private readonly StateSwitcher _stateSwitcher;
		private readonly PausePresenter _pausePresenter;
		private readonly UiSounds _uiSounds;
		private readonly GameplaySounds _gameplaySounds;
		private readonly SaveLoadSystem _saveLoadSystem;
		private readonly PauseService _pauseService;

		public PauseState(StateSwitcher stateSwitcher,
				PausePresenter pausePresenter,
				UiSounds uiSounds,
				GameplaySounds gameplaySounds,
				SaveLoadSystem saveLoadSystem,
				PauseService pauseService
				
		) : base(stateSwitcher) {
			_stateSwitcher = stateSwitcher;
			_pausePresenter = pausePresenter;
			_uiSounds = uiSounds;
			_gameplaySounds = gameplaySounds;
			_saveLoadSystem = saveLoadSystem;
			_pauseService = pauseService;
		}

		public override void Enter() {
			Debug.Log("Pause");
			_pauseService.SetPause(true); 
			
			_pausePresenter.enabled = true;
			_pausePresenter.viewEvents.isMute = _saveLoadSystem.LoadMuteStateFromPrefs();
			_pausePresenter.viewEvents.ContinueEvent += SwitchToGameplay;
			_pausePresenter.viewEvents.MuteChangedEvent += ChangeMute;
		}

		public override void Exit() {
			_pauseService.SetPause(false); 
			
			_pausePresenter.enabled = false;
			_pausePresenter.viewEvents.ContinueEvent -= SwitchToGameplay;
			_pausePresenter.viewEvents.MuteChangedEvent -= ChangeMute;
		}

		private void ChangeMute() {
			var currentMuteState = _saveLoadSystem.LoadMuteStateFromPrefs();
			var isMuted = !currentMuteState;
			_saveLoadSystem.SaveMuteStateFromPrefs(isMuted);
			_pausePresenter.viewEvents.isMute = isMuted;
			_uiSounds.isMute = isMuted;
			_gameplaySounds.isMute = isMuted;
		}

		private void SwitchToGameplay() =>
				_stateSwitcher.SetState<GameplayState>();
	}
}
