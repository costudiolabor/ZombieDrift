using TMPro;
using UnityEngine;
using Cysharp.Threading.Tasks;

namespace Gameplay {
	public class TextHint : View {
		[SerializeField] private TMP_Text textField;

		public async void Show(string message, int showTime) {
			textField.text = message;
			isVisible = true;
			await UniTask.Delay(showTime);
			isVisible = false;
		}
	}
}
