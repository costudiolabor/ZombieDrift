using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Garage {
    public class GarageView : MonoBehaviour {
        [SerializeField] private Button _leftButton;
        [SerializeField] private Button _rightButton;
        [SerializeField] private Button _buyButton;
        [SerializeField] private Button _selectButton;
        [SerializeField] private Button _watchVideoButton;
        [SerializeField] private Button _backButton;
        [SerializeField] private TMP_Text _carPriceText;
        [SerializeField] private TMP_Text _moneyCount;
        [SerializeField] private TMP_Text _purchasedLabel;
        [SerializeField] private GameObject _priceParent;

        public Button leftButton => _leftButton;
        public Button rightButton => _rightButton;
        public Button buyButton => _buyButton;
        public Button selectButton => _selectButton;
        public Button watchVideoButton => _watchVideoButton;
        public Button backButton => _backButton;
        public TMP_Text carPriceText => _carPriceText;
        public TMP_Text moneyCount => _moneyCount;
        public TMP_Text purchasedLabel => _purchasedLabel;

        public bool leftButtonEnabled {
            set => leftButton.gameObject.SetActive(value);
        }
        public bool rightButtonEnabled {
            set => rightButton.gameObject.SetActive(value);
        }
        public bool buyButtonEnabled {
            set => buyButton.gameObject.SetActive(value);
        }
        public bool selectButtonEnabled {
            set => selectButton.gameObject.SetActive(value);
        }
        public bool watchVideoButtonEnabled {
            set => watchVideoButton.gameObject.SetActive(value);
        }
        public bool backButtonEnabled {
            set => backButton.gameObject.SetActive(value);
        }
        public bool carPriceTextEnabled {
            set => carPriceText.gameObject.SetActive(value);
        }
        public bool moneyCountEnabled {
            set => moneyCount.gameObject.SetActive(value);
        }
        public bool purchasedLabelEnabled {
            set => purchasedLabel.gameObject.SetActive(value);
        }
    }
}