using Gameplay;
using Project;
using UnityEngine;
using Object = UnityEngine.Object;
using System.Collections.Generic;

namespace Shop {
    public class ShopPresenter {
        private readonly ScenesLoader _scenesLoader;
        private readonly CarData[] _carDataArray;
        private readonly ProjectCache _projectCache;
        private ShopView _view;
        private CarData _selected;
        private Transform _carSpawnTransform;

        private int _selectedCarIndex, _carsCount, _moneyCount;
        private readonly List<GameObject> _cars = new();
        private readonly HashSet<int> _purchasedCars = new();

        private GameObject _selectedCar;

        private bool isSelectedActive {
            set => _cars[_selectedCarIndex].SetActive(value);
        }

        public ShopPresenter(
            ScenesLoader scenesLoader,
            CarsConfig carsConfig,
            ProjectCache projectCache) {
            _scenesLoader = scenesLoader;
            _carDataArray = carsConfig.cars;
            _projectCache = projectCache;
        }
//todo если куплено скрывать кнопку купить покавывать кнопку выбрать
//todo если куплено вибрано нихера не показывать
        // todo сохранять

        public void Initialize(ShopView shopView, Transform carParent) {
            _view = shopView;
            _view.BackEvent += GotoGameScene;
            _view.BuyEvent += BuyCar;
            _view.SwitchLeftEvent += MoveLeft;
            _view.SwitchRightEvent += MoveRight;
            _view.WatchEvent += WatchAdd;
            _carSpawnTransform = carParent;

            _selectedCarIndex = _projectCache.selectedCarIndex;
            SetUnlockedCars();
            CreateAndHideAllCars();
            Select();
            RefreshMoneyCount();
        }

        private void SetUnlockedCars() {
            _purchasedCars.UnionWith(_projectCache.purchasedCars);
            foreach (var availableIndex in _purchasedCars)
                _carDataArray[availableIndex].isPurchased = true;
        }

        private void RefreshMoneyCount() {
            _moneyCount = _projectCache.moneyCount;
            _view.money = $"${_moneyCount}";
        }

        private void CreateAndHideAllCars() {
            _carsCount = _carDataArray.Length;
            for (var i = 0; i < _carsCount; i++) {
                var car = Object.Instantiate(_carDataArray[i].car.mesh, _carSpawnTransform);
                car.SetActive(false);
                var isCarIsPurchased = _carDataArray[i].isPurchased;
             
                _cars.Add(car);
            }
        }

        private void MoveRight() {
            if (_selectedCarIndex + 1 >= _carsCount)
                return;
            _selectedCarIndex++;
            Select();
        }

        private void MoveLeft() {
            if (_selectedCarIndex <= 0)
                return;
            _selectedCarIndex--;
            Select();
        }
        

        private void Select() {
            if (_selectedCar != null)
                Deselect();

            _selectedCar = _cars[_selectedCarIndex];
            _selectedCar.SetActive(true);
            _view.price = $"${_carDataArray[_selectedCarIndex].price}";
        }

        private void Deselect() {
            _selectedCar.SetActive(false);
            _selectedCar = null;
        }

        private void MoveToDarkLayer(GameObject carObject) {
        }

        private void BuyCar() {
            //    if(_moneyCount>= )
        }

        private void WatchAdd() {
        }

        private void GotoGameScene() =>
            _scenesLoader.SwitchToGameplayScene();
    }
}