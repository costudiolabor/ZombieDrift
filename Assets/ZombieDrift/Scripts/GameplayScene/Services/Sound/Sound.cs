using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Project {
    public class Sound : MonoBehaviour {
        [SerializeField] private AudioSource _audioSource;

        public async void PlayAtPosition(Vector3 position, AudioClip clip, float pitch = 1, float volume = 1) {
            transform.position = position;
            var clipLengthMilliseconds = (clip.length / Mathf.Abs(_audioSource.pitch)) * 1000;
            Play(clip, pitch, volume);
            await UniTask.Delay((int)clipLengthMilliseconds);
            Stop();
        }

        private void Play(AudioClip clip, float pitch = 1, float volume = 1) {
            _audioSource.pitch = pitch;
            _audioSource.clip = clip;
            _audioSource.Play();
        }

        private void Stop() {
            _audioSource.Stop();
            _audioSource.clip = null;
            _audioSource.loop = false;
            _audioSource.pitch = 1;

            gameObject.SetActive(false);
        }
    }
}