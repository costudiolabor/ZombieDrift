using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class SliderCombo : MonoBehaviour{
   [SerializeField] private Slider slider;
   [SerializeField] private Text text;
   [SerializeField] private string textCombo;
   [SerializeField] private float speedSlider = 0.02f;
   //[SerializeField] 
   private int combo;

   private float valueSlider;
   private Coroutine refTimerSlider;
   private void Awake() {
      slider.gameObject.SetActive(false);
      combo = 0;
   }

   public int GetCombo() => combo;
   
   public void SetSlider(float value) {
      slider.value = value;
   }

   public void SetText(int value) {
      text.text = textCombo + value;
   }

   public void ResetSlider() {
      text.text = "";
      slider.gameObject.SetActive(true);
      if (refTimerSlider != null) StopCoroutine(refTimerSlider);
      refTimerSlider = null;
      refTimerSlider = StartCoroutine(TimerSlider());
      combo++;
      if (combo > 0) SetText(combo);
   }
   
   private IEnumerator TimerSlider() {
      valueSlider = 1.0f;
      SetSlider(valueSlider);
      while (valueSlider > 0) {
         yield return new WaitForSeconds(speedSlider);
         valueSlider -= 0.005f;
         SetSlider(valueSlider);
      }
      combo = 0;
      slider.gameObject.SetActive(false);
   }
}
