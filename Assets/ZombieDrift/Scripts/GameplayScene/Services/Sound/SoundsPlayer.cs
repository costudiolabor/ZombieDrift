using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Gameplay;
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

		private bool _zombieVoicesPlaying;

		public SoundsPlayer(SoundConfig soundConfig) =>
				_soundConfig = soundConfig;

		public void Initialize() {
			_soundsParent = new GameObject(POOL_SOUNDS_PARENT_NAME).transform;
			_poolOfSounds = new PoolObjects<Sound>(_soundConfig.soundPrefab, _soundConfig.poolAmount, canExpand: true, _soundsParent);
		}

		/*public void SetZombies(IReadOnlyCollection<Zombie> zombies) {
			_zombies = zombies;
		}*/

		public void PlayZombieHitSoundAtPosition(Vector3 position) {
			var hitSoundsArray = _soundConfig.hitSoundsArray;
			PlayRandomSoundAtPosition(position, hitSoundsArray, randomPinchValue);
		}

		public void PlayCarCrashSoundAtPosition(Vector3 position) {
			var soundsArray = _soundConfig.carCrashArray;
			PlayRandomSoundAtPosition(position, soundsArray, 1);
		}

		public async void StartZombieVoices(IReadOnlyCollection<Zombie> zombies) {
			_zombieVoicesPlaying = true;
			
			var soundsArray = _soundConfig.zombieVoicesArray;
			var frequency = _soundConfig.voiceFrequencyMinMax;

			while (_zombieVoicesPlaying) {
				var activeZombieCount = zombies.Count;
				
				Debug.Log(zombies.Count);

				if (activeZombieCount == 0) {
					StopZombieVoices();
					return;
				}

				int randomIndex = Random.Range(0, activeZombieCount);
				var randomZombie = zombies.ElementAt(randomIndex);
				var randomZombiePosition = randomZombie.transform.position;

				PlayRandomSoundAtPosition(randomZombiePosition, soundsArray, 1);
				var randomDelay = Random.Range(frequency.x, frequency.y);
				await UniTask.Delay(randomDelay);
			}
		}
		public void StopZombieVoices() {
			_zombieVoicesPlaying = false;
		}

		public void StartCarSounds() {
			_engineSound = _poolOfSounds.GetFreeElement();
			_engineSound.PlayLooped(_soundConfig.carEngineClip, 0);

			_tyresSound = _poolOfSounds.GetFreeElement();
			_tyresSound.PlayLooped(_soundConfig.tyresClip, 1);
		}

		public void StopCarSounds() {
			_tyresSound.StopAndDisable();
			_tyresSound = null;

			_engineSound.StopAndDisable();
			_engineSound = null;
		}

		public void UpdateEngine(Vector3 position, float normalizedSpeed, float wheelAxis) {
			if (_engineSound == null)
				throw new Exception("Engine sound is null");
			_engineSound.position = position;
			_engineSound.loopedPitch = normalizedSpeed;

			_tyresSound.position = position;

			//Half tyres volume from engine, another half from wheelAxis
			_tyresSound.volume = (normalizedSpeed + wheelAxis) * 0.5f;
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
