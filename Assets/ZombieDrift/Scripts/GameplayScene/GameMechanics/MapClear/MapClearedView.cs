using TMPro;
using UnityEngine;

namespace Gameplay {
    public class MapClearedView : View {
        [SerializeField] private TMP_Text _mapClearedCaption, _stageClearedCaption;

        public string mapText {
            set => _mapClearedCaption.text = value;
        }

        public bool isMapTextEnabled {
            set => _mapClearedCaption.enabled = value;
        }

        public bool isStageTextEnabled {
            set => _stageClearedCaption.enabled = value;
        }
    }
}