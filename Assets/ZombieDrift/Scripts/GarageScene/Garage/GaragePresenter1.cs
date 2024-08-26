using System.Collections.Generic;
using System.Linq;
using Gameplay;
using NUnit.Framework;
using Project;
using UnityEngine;

namespace Garage {
	public class GaragePresenter1 {
		/*private int moneyCount {
			set => _projectCache.moneyCount = value;
			get => _projectCache.moneyCount;
		}

		private int selectedCarIndex {
			set => _projectCache.selectedCarIndex = value;
			get => _projectCache.selectedCarIndex;
		}*/

		private readonly GarageModel _model;
		//private readonly ProjectCache _projectCache;
		//private readonly HashSet<int> _purchasedCarsIndexes = new();
		private readonly LayerMask _notPurchasedLayerMask;
		private readonly LayerMask _purchasedLayerMask;

	//	private int _carsCount, _currentShopIndex;
		private ShopView _view;
		private Transform carSpawnTransform => _carSpawnTransform;
		private Transform _carSpawnTransform;
		//private GarageItem _selectedGarageItem;
		//private List<GarageItem> _garageData = new();

		public GaragePresenter1(
				CarsConfig carsConfig,
				GarageModel model) {

			_purchasedLayerMask = carsConfig.shopPurchasedLayerMask;
			_notPurchasedLayerMask = carsConfig.shopNotPurchasedLayerMask;
			_model = model;
		}

		public void Initialize(ShopView shopView, Transform carParent) {
			_view = shopView;
			_view.BuyEvent += BuyCar;
			_view.ChooseEvent += Choose;
			_view.SwitchLeftEvent += MoveLeft;
			_view.SwitchRightEvent += MoveRight;
			_carSpawnTransform = carParent;

//			_currentShopIndex = selectedCarIndex;

			Select();
			RefreshMoneyCount();
		}
		
		/*private void SetUnlockedCars() {
			_purchasedCarsIndexes.UnionWith(_projectCache.purchasedCars);
			/*foreach (var purchasedIndex in _purchasedCarsIndexes)
				_carDataArray[purchasedIndex].isPurchased = true;#1#
		}*/

		private void RefreshMoneyCount() =>
				_view.money = $"${_model.moneyCount}";

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
			if (_model.selected != null)
				Deselect();

			_model.selected = _model.items[_currentShopIndex];
			_model.selected.mesh.SetActive(true);
			_view.price = $"${_model.selected.price}";

			var isCarPurchased = _model.purchasedItems.Contains(_currentShopIndex);
			var isChosen = _model.selectedIndex == _model.currentIndex;

			_view.isChosen = isChosen;

			if (!isChosen)
				_view.isPurchased = isCarPurchased;
		}

		private void Deselect() {
			_model.selected.mesh.SetActive(false);
			_model.selected = null;
			_view.price = "0";
		}

		private void BuyCar() {
			var carPrice = _model.selected.price;
			if (moneyCount >= carPrice) {
				moneyCount -= carPrice;
				UnlockCar(_model.items[_currentShopIndex].mesh);
				_model.purchasedItems.Add(_currentShopIndex);
				selectedCarIndex = _currentShopIndex;
				//Save money and selectedIndex
				RefreshMoneyCount();
				Select();
			}
		}
		private void Choose() {
			if (!_model.purchasedItems.Contains(_currentShopIndex)) return;
			selectedCarIndex = _currentShopIndex;
			Select();
		}

		~GaragePresenter1() {
			_view.BuyEvent -= BuyCar;
			_view.SwitchLeftEvent -= MoveLeft;
			_view.SwitchRightEvent -= MoveRight;
		}
	}
}
