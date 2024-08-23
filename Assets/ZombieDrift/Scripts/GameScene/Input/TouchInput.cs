using System;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class TouchInput : IInput {
    public event Action<float> HorizontalAxisChangedEvent;
    public event Action AnyPressedEvent;
    private bool touched => Input.touchCount > 0 && !TryHitHud(touchOne.position);
    private Touch touchOne => Input.touches[0];
    private bool _isTouchedInLastFrame;

    public void Tick() {
        if (_isTouchedInLastFrame) {
            HorizontalAxisChangedEvent?.Invoke(0);
            _isTouchedInLastFrame = false;
        }

        if (Input.touchCount > 0) {
	        Debug.Log("Any pressed");
            AnyPressedEvent?.Invoke();
            _isTouchedInLastFrame = true;
        }

        if (touched) {
            var horizontalCenter = Screen.width / 2;

            if (horizontalCenter > touchOne.position.x)
                HorizontalAxisChangedEvent?.Invoke(-1);
            else
                HorizontalAxisChangedEvent?.Invoke(1);
        }
    }

    private bool TryHitHud(Vector3 mousePosition) {
        var pointerData = new PointerEventData(EventSystem.current) {
            position = mousePosition
        };

        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);
        var distanceToUiElement = results.Count > 0 ? results[0].distance : -1;

        return distanceToUiElement == 0;
    }
}