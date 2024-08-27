using System;

namespace Garage {
    public enum GarageItemState {
        Locked = 0,
        Purchased = 1,
        Selected = 2
    }

    public class Presenter {
        public event Action BuyEvent, ChooseEvent, WatchEvent, PreviousClickedEvent, NextClickedEvent, BackEvent;

        public int price {
            set => _view.carPriceText.text = $"${value}";
        }

        public int money {
            set => _view.moneyCount.text = $"${value}";
        }

        public GarageItemState state {
            set {
                switch (value) {
                    case GarageItemState.Locked:
                        _view.buyButtonEnabled = true;
                        _view.watchVideoButtonEnabled = true;
                        _view.carPriceTextEnabled = true;
                        _view.selectButtonEnabled = false;
                        _view.purchasedLabelEnabled = false;
                        break;
                    case GarageItemState.Purchased:
                        _view.buyButtonEnabled = false;
                        _view.watchVideoButtonEnabled = false;
                        _view.carPriceTextEnabled = false;
                        _view.selectButtonEnabled = true;
                        break;
                    case GarageItemState.Selected:
                        _view.buyButtonEnabled = false;
                        _view.selectButtonEnabled = false;
                        _view.watchVideoButtonEnabled = false;
                        _view.carPriceTextEnabled = false;
                        _view.purchasedLabelEnabled = true;
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

        ~Presenter() {
            _view.leftButton.onClick.RemoveListener(TurnLeftNotify);
            _view.rightButton.onClick.RemoveListener(TurnRightNotify);
            _view.buyButton.onClick.RemoveListener(BuyNotify);
            _view.watchVideoButton.onClick.RemoveListener(WatchNotify);
            _view.selectButton.onClick.RemoveListener(SelectNotify);
            _view.backButton.onClick.RemoveListener(BackNotify);
        }
    }
}