using System;
using Project;
using Zenject;

namespace Gameplay {
    public class MenuPresenter {
	    private readonly UiSounds _uiSounds;
	    public event Action StartGameEvent, GarageEvent;
        private MainMenuView _view;

        public MenuPresenter(UiSounds uiSounds) {
	        _uiSounds = uiSounds;
        }
        public bool enabled {
            set {
                if (value)
                    _view.Appear();
                else
                    _view.Disappear();
            }
        }

        private bool _enabled;

        public void Initialize(MainMenuView view) {
            _view = view;
            _view.StartGameClickedEvent += StartGameNotify;
            _view.GarageClickedEvent += GarageClickedNotify;
        }

        private void GarageClickedNotify() {
	        _uiSounds.PlayClickSound();
	        GarageEvent?.Invoke();
        }

        private void StartGameNotify() =>
            StartGameEvent?.Invoke();

        ~MenuPresenter() {
            _view.StartGameClickedEvent -= StartGameNotify;
            _view.GarageClickedEvent -= GarageClickedNotify;
        }
    }
}