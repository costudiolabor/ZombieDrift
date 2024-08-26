using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Garage {
	
	public class ShopView : MonoBehaviour {
		public event Action BuyEvent, ChooseEvent, WatchEvent, SwitchLeftEvent, SwitchRightEvent, BackEvent;

		[SerializeField] private Button _leftButton, _rightButton, _buyButton, _selectButton, _watchVideoButton, _backButton;
		[SerializeField] private TMP_Text _carPriceText, _moneyCount, _purchacedLabel;

		public string price {
			set => _carPriceText.text = value;
		}

		public string money {
			set => _moneyCount.text = value;
		}

		public bool isPurchased {
			set {
				_buyButton.gameObject.SetActive(!value);
				_watchVideoButton.gameObject.SetActive(!value);
				_carPriceText.gameObject.SetActive(!value);
				_selectButton.gameObject.SetActive(value);
			}
		}
		public bool isChosen {
			set {
				_buyButton.gameObject.SetActive(!value);
				_selectButton.gameObject.SetActive(!value);
				_watchVideoButton.gameObject.SetActive(!value);
				_carPriceText.gameObject.SetActive(!value);
				_purchacedLabel.gameObject.SetActive(value);
			}
		}

		private void WatchNotify() =>
				WatchEvent?.Invoke();

		private void BuyNotify() =>
				BuyEvent?.Invoke();

		private void TurnRightNotify() =>
				SwitchRightEvent?.Invoke();

		private void TurnLeftNotify() =>
				SwitchLeftEvent?.Invoke();

		private void BackNotify() =>
				BackEvent?.Invoke();

		private void SelectNotify() =>
				ChooseEvent?.Invoke();

		private void OnEnable() {
			_leftButton.onClick.AddListener(TurnLeftNotify);
			_rightButton.onClick.AddListener(TurnRightNotify);
			_buyButton.onClick.AddListener(BuyNotify);
			_watchVideoButton.onClick.AddListener(WatchNotify);
			_selectButton.onClick.AddListener(SelectNotify);
			_backButton.onClick.AddListener(BackNotify);
		}

		private void OnDisable() {
			_leftButton.onClick.RemoveListener(TurnLeftNotify);
			_rightButton.onClick.RemoveListener(TurnRightNotify);
			_buyButton.onClick.RemoveListener(BuyNotify);
			_watchVideoButton.onClick.RemoveListener(WatchNotify);
			_selectButton.onClick.RemoveListener(SelectNotify);
			_backButton.onClick.RemoveListener(BackNotify);
		}
	}
}
