using System;
using UnityEngine;

[Serializable]
public class InputPC : IInputControllable {
    private Vector2 _axis;

    public event Action<Vector2> InputControlEvent;
    public void Update() {
        InputControlEvent?.Invoke(GetInputControl());
    }
    
    public Vector2 GetInputControl() {
        _axis.x = Input.GetAxis("Horizontal");
        _axis.y = Input.GetAxis("Vertical");
        return _axis;
    }
}
