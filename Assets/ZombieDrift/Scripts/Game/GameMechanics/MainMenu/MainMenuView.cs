using System;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuView : View {
    public event Action StartGameClickedEvent, GarageClickedEvent;

    [SerializeField] private Button _startButton, _garageButton;
    
    private void OnEnable() =>
        _startButton.onClick.AddListener(StartClickedNotify);

    private void OnDisable() =>
        _garageButton.onClick.AddListener(GarageButtonClickedNotify);

    private void GarageButtonClickedNotify() =>
        GarageClickedEvent?.Invoke();

    private void StartClickedNotify() =>
        StartGameClickedEvent?.Invoke();
}