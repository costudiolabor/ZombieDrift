using System;
using UnityEngine;

public interface IInputControllable {
    public event Action<Vector2> InputControlEvent;
    public Vector2 GetInputControl();
    public void Update();
}
