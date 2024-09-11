using System;
using UnityEngine;

namespace Garage {
	public enum GarageItemState {
		Locked = 0,
		NotEnoughMoney = 1,
		Purchased = 2,
		Selected = 3
	}

	public class GaragePresenter {
		public event Action BuyEvent, ChooseEvent, WatchEvent, PreviousClickedEvent, NextClickedEvent, BackEvent;

		public int carPrice {
			set => _view.carPriceText.text = $"Купить за {value}";
		}

		public int money {
			set => _view.moneyCount.text = $"{value}";
		}

		public float comboMultiplier {
			set {
				_view.comboMultiplier.text = $"Combo Multiplier + {value}";
				_view.isComboMultiplierEnabled = value != 0;
			}
		}
		public float comboDelay {
			set {
				_view.comboDelay.text = $"Combo Delay + {value} sec";
				_view.isComboDelayEnabled = value != 0;
			}
		}

		public GarageItemState state {
			set {
				switch (value) {
					case GarageItemState.Locked:
						_view.isBuyControlActive = true;
						_view.isBuyControlInteractable = true;
						_view.isWatchControlActive = true;
						_view.isSelectControlEnabled = false;
						_view.isSelectedControlActive = false;
						_view.isLockVisible = true;

						break;
					case GarageItemState.NotEnoughMoney:
						_view.isBuyControlActive = true;
						_view.isBuyControlInteractable = false;
						_view.isWatchControlActive = true;
						_view.isSelectControlEnabled = false;
						_view.isSelectedControlActive = false;
						_view.isLockVisible = true;

						break;
					case GarageItemState.Purchased:
						_view.isBuyControlActive = false;
						_view.isWatchControlActive = false;
						_view.isSelectedControlActive = false;
						_view.isSelectControlEnabled = true;
						_view.isLockVisible = false;

						break;
					case GarageItemState.Selected:
						_view.isBuyControlActive = false;
						_view.isSelectControlEnabled = false;
						_view.isWatchControlActive = false;
						_view.isSelectedControlActive = true;
						_view.isLockVisible = false;

						break;
					default:
						throw new ArgumentOutOfRangeException(nameof(value), value, null);
				}
			}
		}

		private GarageView _view;

		public void Initialize(GarageView garageView) {
			_view = garageView;
			_view.leftButton.onClick.AddListener(TurnLeftNotify);
			_view.rightButton.onClick.AddListener(TurnRightNotify);
			_view.buyButton.onClick.AddListener(BuyNotify);
			_view.watchVideoButton.onClick.AddListener(WatchNotify);
			_view.selectButton.onClick.AddListener(SelectNotify);
			_view.backButton.onClick.AddListener(BackNotify);
		}

		private void WatchNotify() =>
				WatchEvent?.Invoke();

		private void BuyNotify() =>
				BuyEvent?.Invoke();

		private void TurnRightNotify() =>
				NextClickedEvent?.Invoke();

		private void TurnLeftNotify() =>
				PreviousClickedEvent?.Invoke();

		private void BackNotify() =>
				BackEvent?.Invoke();

		private void SelectNotify() =>
				ChooseEvent?.Invoke();

		~GaragePresenter() {
			_view.leftButton.onClick.RemoveListener(TurnLeftNotify);
			_view.rightButton.onClick.RemoveListener(TurnRightNotify);
			_view.buyButton.onClick.RemoveListener(BuyNotify);
			_view.watchVideoButton.onClick.RemoveListener(WatchNotify);
			_view.selectButton.onClick.RemoveListener(SelectNotify);
			_view.backButton.onClick.RemoveListener(BackNotify);
		}
	}
}
