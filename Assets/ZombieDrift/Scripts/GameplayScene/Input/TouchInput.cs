using System;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class TouchInput : IInput {
    public event Action<float> HorizontalAxisChangedEvent;
    public event Action AnyPressedEvent;
    private bool touched => Input.touchCount > 0 && !TryHitHud(touchOne.position);
    private Touch touchOne => Input.touches[0];
    private Touch touchTwo => Input.touches[1];
    private bool _isTouchedInLastFrame;

    public void Tick() {
        /*
        if (_isTouchedInLastFrame) {
            HorizontalAxisChangedEvent?.Invoke(0);
            _isTouchedInLastFrame = false;
        }
        */
        if (!touched) {
            HorizontalAxisChangedEvent?.Invoke(0);
            return;
        }
        /*if (touched) {
            //     _isTouchedInLastFrame = true;
        }*/
        
        /*if (!touched) 
            return;*/

        AnyPressedEvent?.Invoke();
        var horizontalCenter = Screen.width / 2;

        //Было залипание если нажать два тача сразу и один отпусить
        // решил поворачивать руль всегда по последнему нажанитю для этого проверяю время
        Touch lastTouch = touchOne;

        if (Input.touchCount == 2) {
            lastTouch = touchOne.deltaTime < touchTwo.deltaTime
                ? touchOne
                : touchTwo;
        }

        if (horizontalCenter > lastTouch.position.x)
            HorizontalAxisChangedEvent?.Invoke(-1);
        else
            HorizontalAxisChangedEvent?.Invoke(1);
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