using System;
using Project;

namespace Gameplay {
    public class GetReadyPresenter {
	    public event Action GoToMenuEvent, GoToPlayEvent;
	    
	    
	    private readonly UiSoundsPlayer _uiSoundsPlayer;
        private readonly IInput _input;
        private GetReadyView _view;
        public bool enabled {
	        set => _view.isActive = value;
        }
        public GetReadyPresenter(IInput input, UiSoundsPlayer uiSoundsPlayer) {
	        _uiSoundsPlayer = uiSoundsPlayer; 
	        _input = input;
        }
        
        public void Initialize(GetReadyView view) {
	        _view = view;
	        _view.BackClickedEvent += GoToMenuNotify;
	        _input.AnyPressedEvent += GoToPlayNotify;
        }
        private void GoToMenuNotify() {
	        _uiSoundsPlayer.PlayClickSound();
	        GoToMenuEvent?.Invoke();
        }

        private void GoToPlayNotify() =>
            GoToPlayEvent?.Invoke();
        
        ~GetReadyPresenter() {
            _view.BackClickedEvent -= GoToMenuNotify;
            _input.AnyPressedEvent -= GoToPlayNotify;
        }
    }
}