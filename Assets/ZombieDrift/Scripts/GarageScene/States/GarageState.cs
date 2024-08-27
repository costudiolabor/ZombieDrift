using Gameplay;
using Project;
using UnityEngine;

namespace Garage {
    public class GarageState : State {
        private readonly ScenesLoader _scenesLoader;
        private readonly RotatablePodium _rotatablePodium;
        private readonly ItemsSwitcher _itemsSwitcher;
        private readonly Presenter _presenter;
        private readonly ProjectCache _projectCache;

        private int moneyCount {
            set => _projectCache.moneyCount = value;
            get => _projectCache.moneyCount;
        }

        private int selectedCarIndex {
            set => _projectCache.selectedCarIndex = value;
            get => _projectCache.selectedCarIndex;
        }

        private GarageItem selectedItem => _itemsSwitcher.selected;
        private int currentIndex => _itemsSwitcher.currentIndex;
        private readonly LayerMask _purchasedLayerMask;

        public GarageState(
            StateSwitcher stateSwitcher,
            ScenesLoader scenesLoader,
            RotatablePodium rotatablePodium,
            ItemsSwitcher itemsSwitcher,
            Presenter presenter,
            ProjectCache projectCache,
            CarsConfig config) : base(stateSwitcher) {
            _purchasedLayerMask = config.purchasedLayerMask;
            _scenesLoader = scenesLoader;
            _rotatablePodium = rotatablePodium;
            _itemsSwitcher = itemsSwitcher;
            _presenter = presenter;
            _projectCache = projectCache;
        }

        public override void Enter() {
            _presenter.NextClickedEvent += ChooseNext;
            _presenter.BackEvent += GotoGameScene;
            _presenter.BuyEvent += BuyCar;
            _presenter.ChooseEvent += Choose;
            _presenter.PreviousClickedEvent += ChoosePrevious;
            
            _itemsSwitcher.BeforeSelectEvent += Deselect;
            _itemsSwitcher.SelectedChangedEvent += Select;
            _itemsSwitcher.Select(selectedCarIndex);
        }

        public override void Exit() {
            _itemsSwitcher.BeforeSelectEvent -= Deselect;
            _itemsSwitcher.SelectedChangedEvent -= Select;
            _presenter.PreviousClickedEvent -= ChoosePrevious;
            _presenter.NextClickedEvent -= ChooseNext;
            _presenter.BackEvent -= GotoGameScene;
            _presenter.BuyEvent -= BuyCar;
            _presenter.ChooseEvent -= Choose;
        }

        public override void FixedTick() {
            _rotatablePodium.RotateAround();
        }

        private void ChooseNext() =>
            _itemsSwitcher.MoveNext();

        private void ChoosePrevious() =>
            _itemsSwitcher.MovePrevious();

        private void Select() {
            selectedItem.mesh.SetActive(true);
            _presenter.price = selectedItem.price;
            _presenter.money = moneyCount;

            var isCarPurchased = _projectCache.purchasedCars.Contains(currentIndex);
            var isChosen = currentIndex == selectedCarIndex;

            if (isChosen)
                _presenter.state = GarageItemState.Selected;
            else if (isCarPurchased)
                _presenter.state = GarageItemState.Purchased;
            else
                _presenter.state = GarageItemState.Locked;
        }

        private void Deselect() =>
            selectedItem.mesh.SetActive(false);

        private void BuyCar() {
            var carPrice = selectedItem.price;

            if (moneyCount < carPrice)
                return;

            moneyCount -= carPrice;
            UnlockCar(selectedItem.mesh);

            _projectCache.purchasedCars.Add(currentIndex);
            selectedCarIndex = currentIndex;
            //Save money and selectedIndex

            Select();
        }

        private void Choose() {
            if (!_projectCache.purchasedCars.Contains(currentIndex))
                return;
            selectedCarIndex = currentIndex;
            Select();
        }

        private void UnlockCar(GameObject car) =>
            Utils.MoveAllChildrenToLayer(car.transform, _purchasedLayerMask);

        private void GotoGameScene() =>
            _scenesLoader.SwitchToGameplayScene();
    }
}