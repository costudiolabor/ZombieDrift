using Project;
namespace Garage {

	public class SelectionState : State {
		private readonly ScenesLoader _scenesLoader;
		private readonly GarageItemsSwitcher _garageSwitcher;
		private readonly ProjectCache _projectCache;

		private int moneyCount {
			set => _projectCache.moneyCount = value;
			get => _projectCache.moneyCount;
		}

		private int selectedCarIndex {
			set => _projectCache.selectedCarIndex = value;
			get => _projectCache.selectedCarIndex;
		}

		public SelectionState(
				StateSwitcher stateSwitcher,
				ScenesLoader scenesLoader,
				GarageItemsSwitcher garageSwitcher,
				ProjectCache projectCache) : base(stateSwitcher) {
			_scenesLoader = scenesLoader;
			_garageSwitcher = garageSwitcher;
			_projectCache = projectCache;
		}

		public override void Enter() {
			_garageSwitcher.BeforeSelectEvent += Deselect;
			_garageSwitcher.SelectedChangedEvent += Select;
		}

		private void RefreshMoneyCount() =>
				_view.money = $"${_model.moneyCount}";

		private void UnlockCar(GameObject car) =>
				Utils.MoveAllChildrenToLayer(car.transform, _purchasedLayerMask);

		private void ChooseNext() {
			_garageSwitcher.MoveNext();
		}

		private void ChoosePrevious() {
			_garageSwitcher.MovePrevious();
		}

		private void Select() {
			_garageSwitcher.selected.mesh.SetActive(true);
			_view.price = $"${_model.selected.price}";

			var isCarPurchased = _garageSwitcher.purchasedItems.Contains(_garageSwitcher.selectedIndex);
			var isChosen = _model.selectedIndex == _model.currentIndex;

			_view.isChosen = isChosen;

			if (!isChosen)
				_view.isPurchased = isCarPurchased;
		}

		private void Deselect() {
			_garageSwitcher.selected.mesh.SetActive(false);
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


		private void GotoGameScene() =>
				_scenesLoader.SwitchToGameplayScene();

		//	private void UnlockCar(GameObject car) =>
		//			Utils.MoveAllChildrenToLayer(car.transform, _purchasedLayerMask);
	}
}
