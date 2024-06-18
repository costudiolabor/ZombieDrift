using System;
using UnityEngine;

[Serializable]
public class Combo {
    [SerializeField] private SliderCombo sliderCombo;
    public void ResetSlider() => sliderCombo.ResetSlider();
    
}

