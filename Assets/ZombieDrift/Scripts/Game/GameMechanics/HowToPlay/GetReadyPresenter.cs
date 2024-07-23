using System;

public class GetReadyPresenter {
    public event Action GoToMenuEvent, GoToPlayEvent;
    private readonly IInput _input;
    private GetReadyView _view;

    public bool enabled {
        set {
            _view.isVisible = value;

            if (value) {
                _view.BackClickedEvent += GoToMenuNotify;
                _input.AnyPressedEvent += GoToPlayNotify;
            }
            else {
                _view.BackClickedEvent -= GoToMenuNotify;
                _input.AnyPressedEvent -= GoToPlayNotify;
            }
        }
    }


    public GetReadyPresenter(IInput input) {
        _input = input;
    }

    private void GoToMenuNotify() {
        GoToMenuEvent?.Invoke();
    }
    
    private void GoToPlayNotify() {
        GoToPlayEvent?.Invoke();
    }

    public void Initialize(GetReadyView view) {
        _view = view;
    }
}