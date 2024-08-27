using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class FadeLoadingView : MonoBehaviour {
	private const int DEFAULT_FADE_TIME = 20;
	[SerializeField] private CanvasGroup canvasGroup;
	[SerializeField] private Image fadeImage;
	[SerializeField] private Color color;
	private float alpha {
		get => canvasGroup.alpha;
		set => canvasGroup.alpha = value;
	}

	private bool isEnabled {
		set => gameObject.SetActive(value);
	}

	public async UniTask FadeIn(int timeMilliseconds = DEFAULT_FADE_TIME) {
		alpha = 0;
		isEnabled = true;
		var iteration = 1 / (float)timeMilliseconds;

		for (float i = 0; i < timeMilliseconds; i++) {
			alpha += iteration;
			await UniTask.Delay(1);
		}
	}

	public async void FadeOut(int timeMilliseconds = DEFAULT_FADE_TIME) {
		alpha = 1;

		var iteration = 1 / (float)timeMilliseconds;
		for (float i = 0; i < timeMilliseconds; i++) {
			alpha -= iteration;
			await UniTask.Delay(1);
		}
		isEnabled = false;
	}


#if UNITY_EDITOR
	private void OnValidate() =>
			fadeImage.color = color;

	#endif
}
