using System;
using Project;
using Zenject;

namespace Gameplay {
    public class MenuPresenter {
	    private readonly UiSoundsPlayer _uiSoundsPlayer;
	    public event Action StartGameEvent, GarageEvent;
        private MainMenuView _view;

        public MenuPresenter(UiSoundsPlayer uiSoundsPlayer) {
	        _uiSoundsPlayer = uiSoundsPlayer;
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
	        _uiSoundsPlayer.PlayClickSound();
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