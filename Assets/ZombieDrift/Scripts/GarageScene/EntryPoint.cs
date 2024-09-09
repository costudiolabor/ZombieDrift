using UnityEngine;
using Zenject;

namespace Garage {
    public class EntryPoint : MonoBehaviour, IInitializable {
        [SerializeField] private GarageView _garageView;
        [SerializeField] private Transform _carParent;
        [SerializeField] private ParticleSystem _buyParticles;
        [SerializeField] private ParticleSystem _selectParticles;

        private GarageScenario _garageScenario;
        private GaragePresenter _garageGaragePresenter;

        [Inject]
        public void Construct(
            GarageScenario garageScenario,
            GaragePresenter garageGaragePresenter,
            Podium podium) {
            _garageScenario = garageScenario;
            _garageGaragePresenter = garageGaragePresenter;
            _garageGaragePresenter.Initialize(_garageView);
            podium.Initialize(_carParent, _buyParticles, _selectParticles);
        }

        public void Initialize() =>
            _garageScenario.Start();
    }
}