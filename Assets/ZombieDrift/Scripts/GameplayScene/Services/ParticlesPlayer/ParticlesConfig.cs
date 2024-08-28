using UnityEngine;

namespace Gameplay {
    [CreateAssetMenu(fileName = "ParticlesConfig", menuName = "Configs/ParticlesConfig")]
    public class ParticlesConfig : ScriptableObject {
        [SerializeField] private ParticleSystem _blood;
        [SerializeField] private ParticleSystem _carHit;
        [SerializeField] private ParticleSystem _carExplode;
        [SerializeField] private ParticleSystem _confetti;

        public ParticleSystem blood => _blood;
        public ParticleSystem carHit => _carHit;
        public ParticleSystem carExplode => _carExplode;
        public ParticleSystem confetti => _confetti;
    }
}