using Project;
using UnityEngine;

namespace Gameplay {
    public class TextHintSystem {
        private const string PARTICLES_PARENT_NAME = "HintParent";

        private readonly PoolObjects<TextHint> _hintPool;
        private readonly int _showTimeMilliseconds;

        private Camera _camera;

        public TextHintSystem(TextHintConfig config) {
            var particlesParent = new GameObject(PARTICLES_PARENT_NAME);
            _showTimeMilliseconds = config.showTimeMillisecondsMilliseconds;
            _hintPool = new PoolObjects<TextHint>(config.textHintPrefab, config.poolSize, true, particlesParent.transform);
        }

        public void Initialize(Camera mainCamera) =>
            _camera = mainCamera;

        public void ShowHint(Vector3 position, string hintText) {
            var hint = _hintPool.GetFreeElement();
            hint.position = position;
          
            var lookPos = hint.position - _camera.transform.position;
            hint.transform.LookAt(lookPos);

            hint.Show(hintText, _showTimeMilliseconds);
        }
    }
}