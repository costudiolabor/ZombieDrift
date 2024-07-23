using System;
using Zenject;

public class LosePresenter {
    public event Action RestartEvent, RepairEvent;
    private LoseView _view;

    public bool enabled {
        set {
            _view.isVisible = value;
            if (value) {
                _view.RestartClickedEvent += RestartNotify;
                _view.RepairClickedEvent += RepairNotify;
            }
            else {
                _view.RestartClickedEvent -= RestartNotify;
                _view.RepairClickedEvent -= RepairNotify;
            }
        }
    }

    public void Initialize(LoseView view) {
        _view = view;
    }

    private void RepairNotify() {
        RepairEvent?.Invoke();
    }

    private void RestartNotify() {
        RestartEvent?.Invoke();
    }
}