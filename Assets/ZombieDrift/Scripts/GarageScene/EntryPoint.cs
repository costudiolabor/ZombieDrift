using Gameplay;
using UnityEngine;
using Zenject;

namespace Garage {
    public class EntryPoint : MonoBehaviour, IInitializable {
        [SerializeField] private GarageView _garageView;
        [SerializeField] private Transform _carParent;
        [SerializeField] private ParticleSystem _buyParticles;
        [SerializeField] private ParticleSystem _selectParticles;
        [SerializeField] private Camera _mainCamera;

        private GarageScenario _garageScenario;

        [Inject]
        public void Construct(
            GarageScenario garageScenario,
            GaragePresenter garageGaragePresenter,
            TextHintSystem textHintSystem,
            Podium podium) {
            _garageScenario = garageScenario;
            garageGaragePresenter.Initialize(_garageView);
            textHintSystem.Initialize(_mainCamera);
            podium.Initialize(_carParent, _buyParticles, _selectParticles);
        }

        public void Initialize() =>
            _garageScenario.Start();
    }
}