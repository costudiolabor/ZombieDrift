using Gameplay;
using Project;
using UnityEngine;

namespace Shop {
	public class ShopPresenter {
		private readonly ScenesLoader _scenesLoader;
		private readonly ProjectCache _projectCache;
		private readonly CarData[] _cars;
		private ShopView _view;
		private CarData _selected;
		private Transform _carParent;
		private int _selectedCarIndex, _carsCount;

		public ShopPresenter(
				ScenesLoader scenesLoader,
				CarsConfig carsConfig,
				ProjectCache projectCache) {
			_scenesLoader = scenesLoader;
			_projectCache = projectCache;
			_cars = carsConfig.cars;
		}

		public void Initialize(ShopView shopView, Transform carParent) {
			_view = shopView;
			_view.BackEvent += GotoGameScene;
			_view.BuyEvent += BuyCar;
			_view.SwitchLeftEvent += SwitchLeft;
			_view.SwitchRightEvent += SwitchRight;
			_view.WatchEvent += WatchAdd;
			_carParent = carParent;
		}

		private CarData FindSelected() {
			foreach (var carData in _cars) {
				if (carData.isSelected)
					return carData;
			}
			return _cars[0];
		}

		public void Show() {
			_selectedCarIndex = _projectCache.selectedCarIndex;
			_carsCount = _cars.Length;
			ShowSelectedCar();
		}

		private void WatchAdd() {

		}

		private void SwitchRight() {
			if (_selectedCarIndex + 1 >= _carsCount)
				return;
			_selectedCarIndex++;
			ShowSelectedCar();
		}

		private void SwitchLeft() {
			if (_selectedCarIndex <= 0)
				return;
			_selectedCarIndex--;
			ShowSelectedCar();
		}

		private void BuyCar() {
		}

		private void ShowSelectedCar() {
			if (_selected != null && _selected.car != null)
				DeleteCar();
			_selected.car = CreateCar(_selectedCarIndex);
		}
		private Car CreateCar(int index) =>
				UnityEngine.Object.Instantiate(_cars[index].car, _carParent);

		private void DeleteCar() {
			UnityEngine.Object.Destroy(_selected.car);
		}
		private void GotoGameScene() {
			_scenesLoader.SwitchToGameplayScene();
		}
	}
}
