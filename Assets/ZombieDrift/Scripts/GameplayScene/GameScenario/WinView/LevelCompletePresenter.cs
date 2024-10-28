using System;
using Project;
namespace Gameplay {
	public class LevelCompletePresenter {
		private readonly UiSoundsPlayer _uiSoundsPlayer;
		public event Action ContinueEvent;

		private WinView _view;

		public LevelCompletePresenter(UiSoundsPlayer uiSoundsPlayer) {
			_uiSoundsPlayer = uiSoundsPlayer;
		}

		public bool enabled {
			set {
				if (value)
					_uiSoundsPlayer.PlayWinSound();
				_view.isActive = value;
			}
		}

		public string completeMessage {
			set => _view.mapText = value;
		}

		public void Initialize(WinView winView) {
			_view = winView;
			_view.ContinueButtonClickedEvent += ContinueNotify;
		}
		private void ContinueNotify() {
			_uiSoundsPlayer.PlayClickSound();
			ContinueEvent?.Invoke();
		}
	}
}
