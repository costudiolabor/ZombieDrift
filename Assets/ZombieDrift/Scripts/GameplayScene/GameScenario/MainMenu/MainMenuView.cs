using System;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay {
    public class MainMenuView : View {
        public event Action StartGameClickedEvent, GarageClickedEvent;

        [SerializeField] private Button _startButton, _garageButton;

        private void OnEnable() {
            _startButton.onClick.AddListener(StartClickedNotify);
            _garageButton.onClick.AddListener(GarageButtonClickedNotify);
        }

        private void OnDisable() {
            _startButton.onClick.RemoveListener(StartClickedNotify);
            _garageButton.onClick.RemoveListener(GarageButtonClickedNotify);
        }

        private void GarageButtonClickedNotify() =>
            GarageClickedEvent?.Invoke();

        private void StartClickedNotify() =>
            StartGameClickedEvent?.Invoke();
    }
}