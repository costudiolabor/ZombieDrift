using System;
using Project;
using Zenject;

namespace Gameplay {
    public class LosePresenter {
	    private readonly UiSoundsPlayer _uiSoundsPlayer;
	    public event Action RestartEvent, RepairEvent;
        private LoseView _view;

        public LosePresenter(UiSoundsPlayer uiSoundsPlayer) {
	        _uiSoundsPlayer = uiSoundsPlayer;
        }
        
        public bool enabled {
	        set {
		        if(value)
			        _uiSoundsPlayer.PlayLoseSound();;
		        _view.isActive = value;
	        }
        }

        public void Initialize(LoseView view) {
            _view = view;
            _view.RestartClickedEvent += RestartNotify;
            _view.RepairClickedEvent += RepairNotify;
        }

        private void RepairNotify() {
	        _uiSoundsPlayer.PlayRepairSound();;
	        RepairEvent?.Invoke();
        }

        private void RestartNotify() {
	        _uiSoundsPlayer.PlayClickSound();
	        RestartEvent?.Invoke();
        }

        ~LosePresenter() {
            _view.RestartClickedEvent -= RestartNotify;
            _view.RepairClickedEvent -= RepairNotify;
        }
    }
}