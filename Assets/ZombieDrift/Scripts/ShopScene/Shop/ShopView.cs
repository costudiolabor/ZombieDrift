using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Shop {
    public class ShopView : MonoBehaviour {
        public event Action BuyEvent, WatchEvent, SwitchLeftEvent, SwitchRightEvent, BackEvent;

        [SerializeField] private Button _leftButton, _rightButton, _buyButton, _watchVideoButton, _backButton;
        [SerializeField] private TMP_Text _carPriceText, _moneyCount;

        public string price {
            set => _carPriceText.text = value;
        }

        public string money {
            set => _moneyCount.text = value;
        }

        private void WatchNotify() =>
            WatchEvent?.Invoke();

        private void BuyNotify() =>
            BuyEvent?.Invoke();

        private void TurnRightNotify() =>
            SwitchLeftEvent?.Invoke();

        private void TurnLeftNotify() =>
            SwitchRightEvent?.Invoke();

        private void BackNotify() =>
            BackEvent?.Invoke();

        private void OnEnable() {
            _leftButton.onClick.AddListener(TurnLeftNotify);
            _rightButton.onClick.AddListener(TurnRightNotify);
            _buyButton.onClick.AddListener(BuyNotify);
            _watchVideoButton.onClick.AddListener(WatchNotify);
            _backButton.onClick.AddListener(BackNotify);
        }

        private void OnDisable() {
            _leftButton.onClick.RemoveListener(TurnLeftNotify);
            _rightButton.onClick.RemoveListener(TurnRightNotify);
            _buyButton.onClick.RemoveListener(BuyNotify);
            _watchVideoButton.onClick.RemoveListener(WatchNotify);
            _backButton.onClick.RemoveListener(BackNotify);
        }
    }
}