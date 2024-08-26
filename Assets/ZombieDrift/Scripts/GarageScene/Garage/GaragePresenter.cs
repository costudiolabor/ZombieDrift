using Gameplay;
using Project;
using UnityEngine;
using Object = UnityEngine.Object;
using System.Collections.Generic;
using Garage;

/*namespace Garage {*/
	public class GaragePresenter {

		private int moneyCount {
			set => _projectCache.moneyCount = value;
			get => _projectCache.moneyCount;
		}

		private int selectedCarIndex {
			set => _projectCache.selectedCarIndex = value;
			get => _projectCache.selectedCarIndex;
		}

		private int selectedCarPrice => _carDataArray[_currentShopIndex].price;

		private readonly ScenesLoader _scenesLoader;
		private readonly CarData[] _carDataArray;
		private readonly ProjectCache _projectCache;
		private readonly List<GameObject> _carModels = new();
		private readonly HashSet<int> _purchasedCarsIndexes = new();
		private readonly LayerMask _notPurchasedLayerMask;
		private readonly LayerMask _purchasedLayerMask;

		private int _carsCount, _currentShopIndex;
		private ShopView _view;
		private Transform _carSpawnTransform;
		private GameObject _selectedCarGameObject;

		public GaragePresenter(
				ScenesLoader scenesLoader,
				CarsConfig carsConfig,
				ProjectCache projectCache) {
			_scenesLoader = scenesLoader;
			_carDataArray = carsConfig.cars;
			_purchasedLayerMask = carsConfig.shopPurchasedLayerMask;
			_notPurchasedLayerMask = carsConfig.shopNotPurchasedLayerMask;
			_projectCache = projectCache;
		}

		public void Initialize(ShopView shopView, Transform carParent) {
			_view = shopView;
			_view.BackEvent += GotoGameScene;
			_view.BuyEvent += BuyCar;
			_view.ChooseEvent += Choose;
			_view.SwitchLeftEvent += MoveLeft;
			_view.SwitchRightEvent += MoveRight;
			_view.WatchEvent += WatchAdd;
			_carSpawnTransform = carParent;

			_currentShopIndex = selectedCarIndex;
			SetUnlockedCars();
			CreateAndHideAllCarModels();
			Select();
			RefreshMoneyCount();
		}

		private void SetUnlockedCars() {
			_purchasedCarsIndexes.UnionWith(_projectCache.purchasedCars);
			/*foreach (var purchasedIndex in _purchasedCarsIndexes)
				_carDataArray[purchasedIndex].isPurchased = true;*/
		}

		private void CreateAndHideAllCarModels() {
			_carsCount = _carDataArray.Length;
			for (var i = 0; i < _carsCount; i++) {
				var carModel = Object.Instantiate(_carDataArray[i].car.mesh, _carSpawnTransform);
				carModel.SetActive(false);

				var isCarIsPurchased = _purchasedCarsIndexes.Contains(i);
				if (!isCarIsPurchased)
					LockCar(carModel);

				_carModels.Add(carModel);
			}
		}
		private void RefreshMoneyCount() =>
				_view.money = $"${moneyCount}";
		private void LockCar(GameObject car) =>
				Utils.MoveAllChildrenToLayer(car.transform, _notPurchasedLayerMask);

		private void UnlockCar(GameObject car) =>
				Utils.MoveAllChildrenToLayer(car.transform, _purchasedLayerMask);

		private void MoveRight() {
			if (_currentShopIndex + 1 >= _carsCount)
				return;
			_currentShopIndex++;
			Select();
		}

		private void MoveLeft() {
			if (_currentShopIndex <= 0)
				return;
			_currentShopIndex--;
			Select();
		}

		private void Select() {
			if (_selectedCarGameObject != null)
				Deselect();

			_selectedCarGameObject = _carModels[_currentShopIndex];
			_selectedCarGameObject.SetActive(true);
			_view.price = $"${selectedCarPrice}";

			var isCarPurchased = _purchasedCarsIndexes.Contains(_currentShopIndex);
			var isChosen = selectedCarIndex == _currentShopIndex;

			_view.isChosen = isChosen;

			if (!isChosen)
				_view.isPurchased = isCarPurchased;
		}

		private void Deselect() {
			_selectedCarGameObject.SetActive(false);
			_selectedCarGameObject = null;
			_view.price = "0";
		}

		private void BuyCar() {
			if (moneyCount >= selectedCarPrice) {
				moneyCount -= selectedCarPrice;
				UnlockCar(_carModels[_currentShopIndex]);
				_purchasedCarsIndexes.Add(_currentShopIndex);
				selectedCarIndex = _currentShopIndex;
				//Save money and selectedIndex
				RefreshMoneyCount();
				Select();
			}
		}

		private void Choose() {
			if (!_purchasedCarsIndexes.Contains(_currentShopIndex)) return;
			selectedCarIndex = _currentShopIndex;
			Select();
		}

		private void WatchAdd() {
		}

		private void GotoGameScene() =>
				_scenesLoader.SwitchToGameplayScene();

		~GaragePresenter() {
			_view.BackEvent -= GotoGameScene;
			_view.BuyEvent -= BuyCar;
			_view.SwitchLeftEvent -= MoveLeft;
			_view.SwitchRightEvent -= MoveRight;
			_view.WatchEvent -= WatchAdd;
		}
	}
//}
