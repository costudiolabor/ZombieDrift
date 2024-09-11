using Gameplay;
using Project;
using UnityEngine;

namespace Garage {
	public class GarageState : State {
		private readonly ScenesLoader _scenesLoader;
		private readonly Podium _podium;
		private readonly ItemsSwitcher _itemsSwitcher;
		private readonly GaragePresenter _garagePresenter;
		private readonly ProjectCache _projectCache;
		private readonly MoneyWallet _moneyWallet;

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
				Podium podium,
				ItemsSwitcher itemsSwitcher,
				GaragePresenter garagePresenter,
				ProjectCache projectCache,
				CarsConfig config,
				MoneyWallet moneyWallet) : base(stateSwitcher) {
			_purchasedLayerMask = config.purchasedLayerMask;
			_scenesLoader = scenesLoader;
			_podium = podium;
			_itemsSwitcher = itemsSwitcher;
			_garagePresenter = garagePresenter;
			_projectCache = projectCache;
			_moneyWallet = moneyWallet;
		}

		public override void Enter() {
			_garagePresenter.NextClickedEvent += ChooseNext;
			_garagePresenter.BackEvent += GotoGameScene;
			_garagePresenter.BuyEvent += BuyCar;
			_garagePresenter.ChooseEvent += Choose;
			_garagePresenter.PreviousClickedEvent += ChoosePrevious;

			_itemsSwitcher.BeforeSelectEvent += Deselect;
			_itemsSwitcher.SelectedChangedEvent += Select;
			_itemsSwitcher.Select(selectedCarIndex);
		}

		public override void Exit() {
			_itemsSwitcher.BeforeSelectEvent -= Deselect;
			_itemsSwitcher.SelectedChangedEvent -= Select;
			_garagePresenter.PreviousClickedEvent -= ChoosePrevious;
			_garagePresenter.NextClickedEvent -= ChooseNext;
			_garagePresenter.BackEvent -= GotoGameScene;
			_garagePresenter.BuyEvent -= BuyCar;
			_garagePresenter.ChooseEvent -= Choose;
		}

		public override void FixedTick() =>
				_podium.RotateAround();

		private void ChooseNext() =>
				_itemsSwitcher.MoveNext();

		private void ChoosePrevious() =>
				_itemsSwitcher.MovePrevious();

		private void Select() {
			selectedItem.mesh.SetActive(true);
			var carPrice = selectedItem.price;
			var moneyCount = _moneyWallet.count;

			var isCarPurchased = _projectCache.purchasedCars.Contains(currentIndex);
			var isChosen = currentIndex == selectedCarIndex;
			_garagePresenter.money = moneyCount;
			_garagePresenter.carPrice = carPrice;
			_garagePresenter.comboMultiplier = selectedItem.comboMultiplier;
			_garagePresenter.comboDelay = selectedItem.comboDelay;

			if (isChosen)
				_garagePresenter.state = GarageItemState.Selected;
			else if (isCarPurchased)
				_garagePresenter.state = GarageItemState.Purchased;
			else if (moneyCount >= carPrice)
				_garagePresenter.state = GarageItemState.Locked;
			else
				_garagePresenter.state = GarageItemState.NotEnoughMoney;
		}

		private void Deselect() =>
				selectedItem.mesh.SetActive(false);

		private void BuyCar() {
			var carPrice = selectedItem.price;

			if (_moneyWallet.count < carPrice)
				return;
			_moneyWallet.SpendCoin(carPrice);
			_podium.PlayBuyParticles();
			UnlockCar(selectedItem.mesh);

			_projectCache.purchasedCars.Add(currentIndex);
			selectedCarIndex = currentIndex;
			//Save money and selectedIndex
			Select();
		}

		private void Choose() {
			if (!_projectCache.purchasedCars.Contains(currentIndex))
				return;
			_podium.PlaySelectParticles();
			selectedCarIndex = currentIndex;
			Select();
		}

		private void UnlockCar(GameObject car) =>
				Utils.MoveAllChildrenToLayer(car.transform, _purchasedLayerMask);

		private void GotoGameScene() =>
				_scenesLoader.SwitchToGameplayScene();
	}
}
