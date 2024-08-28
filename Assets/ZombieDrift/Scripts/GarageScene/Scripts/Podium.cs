using UnityEngine;

namespace Garage {
    public class Podium {
        private const float ROTATION_SPEED_IN_DEGREES_PER_UPDATE = -0.3f;
        public Transform spawnParent => _parent;
        private Transform _parent;
        private ParticleSystem _buyParticles;

        public void Initialize(Transform parentTransform, ParticleSystem buyParticles) {
            _parent = parentTransform;
            _buyParticles = buyParticles;
        }

        public void RotateAround() =>
            _parent.transform.rotation *= Quaternion.AngleAxis(ROTATION_SPEED_IN_DEGREES_PER_UPDATE, Vector3.up);

        public void PlayBuyParticles() =>
            _buyParticles.Play();
    }
}