using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Project {
    public class SoundsPlayer {
        private const string POOL_SOUNDS_PARENT_NAME = "SoundsParent";
        private const float MIN_PINCH = 0.9f;
        private const float MAX_PINCH = 1.1f;

        private readonly SoundConfig _soundConfig;
        private PoolObjects<Sound> _poolOfSounds;
        private Transform _soundsParent;
        private Sound _engineSound, _tyresSound;

        private AudioClip[] _hitSoundsArray;

        private float randomPinchValue => Random.Range(MIN_PINCH, MAX_PINCH);

        public SoundsPlayer(SoundConfig soundConfig) =>
            _soundConfig = soundConfig;

        public void Initialize() {
            _soundsParent = new GameObject(POOL_SOUNDS_PARENT_NAME).transform;
            _poolOfSounds = new PoolObjects<Sound>(_soundConfig.soundPrefab, _soundConfig.poolAmount, canExpand: true, _soundsParent);
        }
        
        public void PlayZombieHitSoundAtPosition(Vector3 position) {
            var hitSoundsArray = _soundConfig.hitSoundsArray;
            PlayRandomSoundAtPosition(position, hitSoundsArray, randomPinchValue);
        }

        public void PlayCarCrashSoundAtPosition(Vector3 position) {
            var soundsArray = _soundConfig.carCrashArray;
            PlayRandomSoundAtPosition(position, soundsArray, 1);
        }

        public void StartCarSounds() {
            _engineSound = _poolOfSounds.GetFreeElement();
            _engineSound.PlayLooped(_soundConfig.carEngineClip,0);
            
            _tyresSound = _poolOfSounds.GetFreeElement();
            _tyresSound.PlayLooped(_soundConfig.tyresClip,0);
        }

        public void StopCarSounds() {
            _tyresSound.StopAndDisable();
            _tyresSound = null;
            
            _engineSound.StopAndDisable();
            _engineSound = null;
        }

        public void UpdateEngine(Vector3 position, float enginePitch, float tyresPitch) {
            if (_engineSound == null)
                throw new Exception("Engine sound is null");
            _engineSound.position = position;
            _engineSound.loopedPitch = enginePitch;
        }
        
        private void PlayRandomSoundAtPosition(Vector3 position, AudioClip[] soundsArray, float pitch) {
            if (soundsArray.Length == 0)
                return;

            int randomIndex = Random.Range(0, soundsArray.Length);
            var audioClip = soundsArray[randomIndex];

            var sound = _poolOfSounds.GetFreeElement();

            sound.PlayAndDisableAtPosition(position, audioClip, pitch);
        }
    }
}