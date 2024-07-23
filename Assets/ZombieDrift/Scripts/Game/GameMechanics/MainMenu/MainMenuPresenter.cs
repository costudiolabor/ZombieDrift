using System;
using Zenject;

public class MainMenuPresenter {
    public event Action StartGameEvent, GarageEvent;
    private MainMenuView _view;

    public bool enabled {
        set {
            _view.isVisible = value;
            if (value) {
                _view.StartGameClickedEvent += StartGameNotify;
                _view.GarageClickedEvent += GarageClickedNotify;
            }
            else {
                _view.StartGameClickedEvent -= StartGameNotify;
                _view.GarageClickedEvent -= GarageClickedNotify;
            }
        }
    }

    public void Initialize(MainMenuView view) {
        _view = view;
    }

    private void GarageClickedNotify() {
        GarageEvent?.Invoke();
    }

    private void StartGameNotify() {
        StartGameEvent?.Invoke();
    }
}