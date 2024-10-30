using System;
using Project;
using Zenject;

namespace Gameplay {
    public class LosePresenter {
	    private readonly UiSounds _uiSounds;
	    public event Action RestartEvent, RepairEvent;
        private LoseView _view;

        public LosePresenter(UiSounds uiSounds) {
	        _uiSounds = uiSounds;
        }
        
        public bool enabled {
	        set {
		        if(value)
			        _uiSounds.PlayLoseSound();;
		        _view.isActive = value;
	        }
        }

        public void Initialize(LoseView view) {
            _view = view;
            _view.RestartClickedEvent += RestartNotify;
            _view.RepairClickedEvent += RepairNotify;
        }

        private void RepairNotify() {
	        _uiSounds.PlayRepairSound();;
	        RepairEvent?.Invoke();
        }

        private void RestartNotify() {
	        _uiSounds.PlayClickSound();
	        RestartEvent?.Invoke();
        }

        ~LosePresenter() {
            _view.RestartClickedEvent -= RestartNotify;
            _view.RepairClickedEvent -= RepairNotify;
        }
    }
}