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

        private int _selectedCarIndex, _carsCount;
        private readonly List<GameObject> _cars = new();
        private HashSet<int> _availableCars = new();

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
//todo Бабки отображать
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

            CreateAndHideAllCars();
        }

        private void CreateAndHideAllCars() {
            _selectedCarIndex = _projectCache.selectedCarIndex;
            _carsCount = _carDataArray.Length;
            _availableCars.UnionWith(_projectCache.availableCars);

            for (var i = 0; i < _carsCount; i++) {
                var car = Object.Instantiate(_carDataArray[i].car.mesh, _carSpawnTransform);
                car.SetActive(false);
                _cars.Add(car);
            }
            Select();
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

        private void BuyCar() {
        }

        private void WatchAdd() {
        }

        private void GotoGameScene() =>
            _scenesLoader.SwitchToGameplayScene();
    }
}