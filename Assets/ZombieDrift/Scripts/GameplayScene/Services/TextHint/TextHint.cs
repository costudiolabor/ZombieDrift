using TMPro;
using UnityEngine;
using Cysharp.Threading.Tasks;

namespace Gameplay {
	public class TextHint : View {
		[SerializeField] private TMP_Text _textField;

		public Vector3 position {
			set => transform.position = value;
			get => transform.position;
		}

		public async void SetText(string message, int showTime) {
			_textField.text = message;

			isActive = true;
			await UniTask.Delay(showTime);
			isActive = false;
		}
	}
}
