using UnityEngine;

namespace Project {
    public class SoundsPlayer {
        private const float MIN_PINCH = 0.9f;
        private const float MAX_PINCH = 1.1f;

        private readonly SoundConfig _soundConfig;
        private PoolObjects<Sound> _poolOfSounds;
        private Transform _soundsParent;

        private AudioClip[] _hitSoundsArray;

        private float randomPinchValue => Random.Range(MIN_PINCH, MAX_PINCH);

        public SoundsPlayer(SoundConfig soundConfig) =>
            _soundConfig = soundConfig;

        public void Initialize() {
            _soundsParent = new GameObject(nameof(_soundsParent)).transform;
            _poolOfSounds = new PoolObjects<Sound>(_soundConfig.soundPrefab, _soundConfig.poolAmount, canExpand: true, _soundsParent);
        }

        public void PlayZombieHitSound(Vector3 position) {
            var hitSoundsArray = _soundConfig.hitSoundsArray;

            if (hitSoundsArray.Length == 0)
                return;

            int randomIndex = Random.Range(0, hitSoundsArray.Length);
            var audioClip = hitSoundsArray[randomIndex];

            var sound = _poolOfSounds.GetFreeElement();
            
            sound.PlayAtPosition(position, audioClip, randomPinchValue);
        }
        
        
        
    }
}