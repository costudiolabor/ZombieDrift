using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Project;
using UnityEngine;
using Zenject;

namespace Gameplay {
	public class TextHintSystem /*: IFixedTickable*/ {
		private const string PARTICLES_PARENT_NAME = "HintParent";

		private readonly PoolObjects<TextHint> _hintPool;
		private readonly int _showTimeMilliseconds;
		private readonly Vector3 _offset;

		private Camera _camera;
		private readonly List<TextHint> _activeHints = new();

		public TextHintSystem(TextHintConfig config) {
			var particlesParent = new GameObject(PARTICLES_PARENT_NAME);
			_showTimeMilliseconds = config.showTimeMillisecondsMilliseconds;
			_hintPool = new PoolObjects<TextHint>(config.textHintPrefab, config.poolSize, true, particlesParent.transform);
			_offset = config.offset;
		}

		public void Initialize(Camera mainCamera) =>
				_camera = mainCamera;

		public /*async*/ void ShowHint(Vector3 position, string hintText) {
			var hint = _hintPool.GetFreeElement();
			hint.position = position + _offset;

			hint.SetText(hintText , _showTimeMilliseconds);
			var lookPos = hint.position - _camera.transform.position;
			hint.transform.LookAt(lookPos);
			/*
			_activeHints.Add(hint);

			hint.isActive = true;
			await UniTask.Delay(_showTimeMilliseconds);
			hint.isActive = false;

			_activeHints.Remove(hint);
		*/
		}

		/*public void FixedTick() {
			if (_activeHints.Count == 0)
				return;

			foreach (var hint in _activeHints) {
				var lookPos = hint.position - _camera.transform.position;
				hint.transform.LookAt(_camera.transform.position);
			}
		}*/
	}
}
