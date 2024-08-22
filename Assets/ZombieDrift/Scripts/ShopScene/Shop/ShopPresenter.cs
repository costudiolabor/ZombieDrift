using Gameplay;
using Project;

namespace Shop {
    public class ShopPresenter {
        private readonly ScenesLoader _scenesLoader;
        private readonly CarsConfig _carsConfig;
        private ShopView _view;
        
        public ShopPresenter(ScenesLoader scenesLoader, CarsConfig carsConfig) {
            _scenesLoader = scenesLoader;
            _carsConfig = carsConfig;
        }

        public void Initialize(ShopView shopView) {
            _view = shopView;
            _view.BackEvent += GotoGameScene;
            _view.BuyEvent += BuyCar;
            _view.SwitchLeftEvent += SwitchLeft;
            _view.SwitchRightEvent += SwitchRight;
            _view.WatchEvent += WatchAdd;
        }

        public void Show() {
            
        }

        private void WatchAdd() {
        }

        private void SwitchRight() {
        }

        private void SwitchLeft() {
        }

        private void BuyCar() {
        }

        private void GotoGameScene() {
            _scenesLoader.SwitchToGameplayScene();
        }
    }
}