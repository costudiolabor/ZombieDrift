using UnityEngine;

namespace Project {
    [CreateAssetMenu(menuName = "Configs/SoundConfig", fileName = "SoundConfig", order = 0)]
    public class SoundConfig : ScriptableObject {
        [SerializeField] private Sound _soundPrefab;
        [SerializeField] private int _poolAmount;

        [SerializeField] private AudioClip[] _zombieSounds;
        [SerializeField] private AudioClip[] _hitSoundsArray;

        public AudioClip[] zombieSounds => _zombieSounds;
        public AudioClip[] hitSoundsArray => _hitSoundsArray;
        public Sound soundPrefab => _soundPrefab;
        public int poolAmount => _poolAmount;
    }
}