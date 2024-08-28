using System;
using Zenject;

namespace Gameplay {
    public class LosePresenter {
        public event Action RestartEvent, RepairEvent;
        private LoseView _view;

        public bool enabled {
            set => _view.isActive = value;
        }

        public void Initialize(LoseView view) {
            _view = view;
            _view.RestartClickedEvent += RestartNotify;
            _view.RepairClickedEvent += RepairNotify;
        }

        private void RepairNotify() =>
            RepairEvent?.Invoke();

        private void RestartNotify() =>
            RestartEvent?.Invoke();

        ~LosePresenter() {
            _view.RestartClickedEvent -= RestartNotify;
            _view.RepairClickedEvent -= RepairNotify;
        }
    }
}