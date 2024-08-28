using System;
using Zenject;

namespace Gameplay {
    public class MenuPresenter {
        public event Action StartGameEvent, GarageEvent;
        private MainMenuView _view;

        public bool enabled {
            set => _view.isActive = value;
        }

        private bool _enabled;

        public void Initialize(MainMenuView view) {
            _view = view;
            _view.StartGameClickedEvent += StartGameNotify;
            _view.GarageClickedEvent += GarageClickedNotify;
        }

        private void GarageClickedNotify() =>
            GarageEvent?.Invoke();
        
        private void StartGameNotify() =>
            StartGameEvent?.Invoke();

        ~MenuPresenter() {
            _view.StartGameClickedEvent -= StartGameNotify;
            _view.GarageClickedEvent -= GarageClickedNotify;
        }
    }
}