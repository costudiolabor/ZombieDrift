using Cysharp.Threading.Tasks;

namespace Gameplay {
    public class HowToPlayPresenter {
        private const int HIDE_DELAY_MILLISECONDS = 1500;
        private HowToPlayView _view;

        public bool enabled {
            set => _view.isVisible = value;
        }

        public void Initialize(HowToPlayView view) =>
            _view = view;

        public async void HideWithDelay() {
            await UniTask.Delay(HIDE_DELAY_MILLISECONDS);
            enabled = false;
        }
    }
}