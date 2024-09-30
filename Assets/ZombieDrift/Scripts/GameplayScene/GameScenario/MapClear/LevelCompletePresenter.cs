using System;
namespace Gameplay {
	public class LevelCompletePresenter {
		public event Action ContinueEvent;

		private WinView _view;

		public bool enabled {
			set => _view.isActive = value;
		}

		public string completeMessage {
			set => _view.mapText = value;
		}


		public void Initialize(WinView winView) {
			_view = winView;
			_view.ContinueButtonClickedEvent += ContinueNotify;
		}
		private void ContinueNotify() =>
				ContinueEvent?.Invoke();
	}
}
