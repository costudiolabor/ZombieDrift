using Project;
using UnityEngine;

namespace Gameplay {
    public class ParticlesPlayer {
        private const int POOL_SIZE = 3;
        private const string PARENT_NAME = "Particles";
        private readonly ParticlesConfig _particlesConfig;
        private readonly Factory _factory;
        private readonly Transform _parent;

        private PoolObjects<ParticleSystem> _bloodPool, _carHitPool;
        private ParticleSystem _confetti;

        private ParticlesPlayer(ParticlesConfig particlesConfig, Factory factory) {
            _particlesConfig = particlesConfig;
            _factory = factory;
    
            _parent = new GameObject(PARENT_NAME).transform;
            CreateParticles();
        }

        private void CreateParticles() {
            _bloodPool = new PoolObjects<ParticleSystem>(_particlesConfig.blood, POOL_SIZE, true, _parent);
            _carHitPool = new PoolObjects<ParticleSystem>(_particlesConfig.carHit, POOL_SIZE, true, _parent);
            _confetti = _factory.Create(_particlesConfig.confetti, _parent);
            _confetti.gameObject.SetActive(false);
        }

        public void PlayWinConfetti(Vector3 hitPosition) {
             _confetti.gameObject.SetActive(true);
             _confetti.transform.position = hitPosition;
        }

        public void PlayObstacleHit(Vector3 hitPosition) {
            var particle = _carHitPool.GetFreeElement();
            particle.transform.position = hitPosition;
        }

        public void PlayZombieHit(Vector3 hitPosition) {
            var particle = _bloodPool.GetFreeElement();

            particle.transform.position = hitPosition;
        }
    }
}