using System;
using UI;
using UnityEngine;
using UnityEngine.UI;

public class PauseView :View {
    public event Action ContinueEvent;
    public event Action<bool> MuteChangedEvent;

    [SerializeField] private ExtendedToggle _toggle;
    [SerializeField] private Button _button;

    public bool isMute {
        set => _toggle.SetIsOnWithoutNotify(value);
    }
    
    private void ContinueNotify() =>
        ContinueEvent?.Invoke();

    private void MuteValueChangedNotify(bool isOn) =>
        MuteChangedEvent?.Invoke(isOn);
    
    private void OnEnable() {
        _toggle.onValueChanged.AddListener(MuteValueChangedNotify);
        _button.onClick.AddListener(ContinueNotify);
    }
    private void OnDisable() {
        _toggle.onValueChanged.RemoveListener(MuteValueChangedNotify);
        _button.onClick.RemoveListener(ContinueNotify);
    }
}