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

		public void PlayZombieHitSoundAtPosition(Vector3 position) {
			var hitSoundsArray = _soundConfig.hitSoundsArray;

			PlayRandomSoundAtPosition(position, hitSoundsArray);
		}

		public void PlayCarCrashSoundAtPosition(Vector3 position) {
			var soundsArray = _soundConfig.carCrashArray;
			PlayRandomSoundAtPosition(position, soundsArray);
		}

		public void PlayZombieVoicesAtPosition(Vector3 position) {
			var zombieVoicesArray = _soundConfig.zombieVoices;

			PlayRandomSoundAtPosition(position, zombieVoicesArray);
		}
		private void PlayRandomSoundAtPosition(Vector3 position, AudioClip[] soundsArray) {
			if (soundsArray.Length == 0)
				return;

			int randomIndex = Random.Range(0, soundsArray.Length);
			var audioClip = soundsArray[randomIndex];

			var sound = _poolOfSounds.GetFreeElement();

			sound.PlayAtPosition(position, audioClip, randomPinchValue);
		}
	}
}
