using UnityEngine;

namespace Gameplay {
    [CreateAssetMenu(menuName = "Configs/TextHintConfig", fileName = "TextHintConfig", order = 0)]
    public class TextHintConfig : ScriptableObject {
        [SerializeField] private TextHint _textHintPrefab;
        [SerializeField] private int _poolSize = 3;
        [SerializeField] private int _showTimeMilliseconds;
        public TextHint textHintPrefab => _textHintPrefab;
        public int poolSize => _poolSize;
        public int showTimeMillisecondsMilliseconds => _showTimeMilliseconds;
    }
}