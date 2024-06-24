using System;
using UnityEngine;

public class KeyboardInput : IInput {
	public event Action<float> HorizontalAxisChangedEvent;
	public event Action AnyPressedEvent;
	
	private bool isLeftPressed => InputUtils.CheckIfAnyKeyPressed(_config.leftKeys);
	private bool isRightPressed => InputUtils.CheckIfAnyKeyPressed(_config.rightKeys);
	private bool isNothingPressed => !isLeftPressed && !isRightPressed;
	private bool isAnyPressed => Input.anyKey;
	
	private readonly InputConfig _config;
	
	private bool _pressedInLastFrame;
	
	public KeyboardInput(InputConfig config) {
		_config = config;
	}

	public void Tick() {
		if (_pressedInLastFrame) {
			HorizontalAxisChangedEvent?.Invoke(0);
			_pressedInLastFrame = false;
		}
		
		if (isAnyPressed) {
			AnyPressedEvent?.Invoke();
			_pressedInLastFrame = true;
		}

		if (isLeftPressed)
			HorizontalAxisChangedEvent?.Invoke(-1);
		if (isRightPressed)
			HorizontalAxisChangedEvent?.Invoke(1);
	}
}