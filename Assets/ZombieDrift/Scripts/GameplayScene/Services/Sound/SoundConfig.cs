using UnityEngine;

namespace Project {
	[CreateAssetMenu(menuName = "Configs/SoundConfig", fileName = "SoundConfig", order = 0)]
	public class SoundConfig : ScriptableObject {
		[SerializeField] private Sound _soundPrefab;
	
		[SerializeField] private int _poolAmount;

		[SerializeField] private Vector2 _voiceFrecuencyMinMax;
		[SerializeField] private AudioClip[] _zombieVoices;
		[SerializeField] private AudioClip[] _hitSoundsArray;
		[SerializeField] private AudioClip[] _carChashArray;
		[SerializeField] private AudioClip _carEngineClip;
		[SerializeField] private AudioClip _tyresClip;
		
		public Vector2 voiceFrequencyMinMax => _voiceFrecuencyMinMax;
		public AudioClip carEngineClip => _carEngineClip;
		public AudioClip tyresClip => _tyresClip;
		public AudioClip[] zombieVoices => _zombieVoices;
		public AudioClip[] hitSoundsArray => _hitSoundsArray;
		public AudioClip[] carCrashArray => _carChashArray;
		public Sound soundPrefab => _soundPrefab;
		public int poolAmount => _poolAmount;
	}
}
