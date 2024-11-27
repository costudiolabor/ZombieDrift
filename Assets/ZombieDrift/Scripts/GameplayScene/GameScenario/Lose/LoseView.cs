using System;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay {
    public class LoseView : View {
        public event Action RestartClickedEvent, RepairClickedEvent;
        [SerializeField] private Button _repairButton, _restartButton;

        public bool isRepairInteractable {
            set => _repairButton.interactable = value;
        }

        private void OnEnable() {
            _repairButton.onClick.AddListener(RepairNotify);
            _restartButton.onClick.AddListener(RestartNotify);
        }

        private void OnDisable() {
            _repairButton.onClick.RemoveListener(RepairNotify);
            _restartButton.onClick.RemoveListener(RestartNotify);
        }

        private void RestartNotify() =>
            RestartClickedEvent?.Invoke();

        private void RepairNotify() =>
            RepairClickedEvent?.Invoke();
    }
}