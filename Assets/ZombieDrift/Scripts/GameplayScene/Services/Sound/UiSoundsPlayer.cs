using UnityEngine;

namespace Project {
	public class UiSoundsPlayer {
		private const string POOL_SOUNDS_PARENT_NAME = "SoundsParent";
		private const float MIN_PINCH = 1f;
		private const float MAX_PINCH = 1.2f;

		private readonly UiSoundConfig _soundConfig;
		private PoolObjects<Sound> _poolOfSounds;
		private Transform _soundsParent;
		
		public UiSoundsPlayer(UiSoundConfig soundConfig) =>
				_soundConfig = soundConfig;
		
		public void Initialize() {
			_soundsParent = new GameObject(POOL_SOUNDS_PARENT_NAME).transform;
			CreatePool();
		}
		
		public void Initialize(Transform poolParent) {
			_soundsParent = poolParent;
			CreatePool();
		}
		private void CreatePool() =>
			_poolOfSounds = new PoolObjects<Sound>(_soundConfig.soundPrefab, _soundConfig.poolAmount, canExpand: true, _soundsParent);

		public void PlayClickSound() {
			var sound = _poolOfSounds.GetFreeElement();
			var randomPitch = Random.Range(MIN_PINCH, MAX_PINCH);
			sound.PlayAndDisable(_soundConfig.buttonAudioClip, randomPitch);
		}
		
		public void PlayCoinSound() {
			var sound = _poolOfSounds.GetFreeElement();
			var randomPitch = Random.Range(MIN_PINCH, MAX_PINCH);
			sound.PlayAndDisable(_soundConfig.coinAudioClip, randomPitch);
		}

		public void PlayLoseSound() {
			var sound = _poolOfSounds.GetFreeElement();
			sound.PlayAndDisable(_soundConfig.loseAudioClip);
		}
		public void PlayWinSound() {
			var sound = _poolOfSounds.GetFreeElement();
			sound.PlayAndDisable(_soundConfig.winAudioClip);
		}
		public void PlayRepairSound() {
			var sound = _poolOfSounds.GetFreeElement();
			sound.PlayAndDisable(_soundConfig.repairAudioClip);
		}
	}
}
