using UnityEngine;
using Zenject;

namespace Garage {
    public class EntryPoint : MonoBehaviour, IInitializable {
        [SerializeField] private GarageView _garageView;
        [SerializeField] private Transform _carParent;

        private GarageScenario _garageScenario;
        private Presenter _garagePresenter;

        [Inject]
        public void Construct(
            GarageScenario garageScenario,
            Presenter garagePresenter,
            RotatablePodium rotatablePodium) {
            _garageScenario = garageScenario;
            _garagePresenter = garagePresenter;
            _garagePresenter.Initialize(_garageView);
            rotatablePodium.Initialize(_carParent);
        }

        public void Initialize() =>
            _garageScenario.Start();
    }
}