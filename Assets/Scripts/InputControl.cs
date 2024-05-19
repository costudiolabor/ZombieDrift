using System;
using UnityEngine;

[Serializable]
public class InputControl : IInputControllable {
    private Vector2 _axis; 
        
    public Vector2 GetInputControl() {
        _axis.x = Input.GetAxis("Horizontal");
        _axis.y = Input.GetAxis("Vertical");
        return _axis;
    }
}
