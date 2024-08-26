using UnityEngine;
using Zenject;

namespace Garage {
	public class EntryPoint : MonoBehaviour, IInitializable {
		[SerializeField] private ShopView _shopView;
		[SerializeField] private Transform _carParent;

		private GarageScenario _garageScenario;
		private GaragePresenter1 _garagePresenter;

		[Inject]
		public void Construct(GarageScenario garageScenario, GaragePresenter1 garagePresenter) {
			_garageScenario = garageScenario;
			_garagePresenter = garagePresenter;
			_garagePresenter.Initialize(_shopView, _carParent);
		}
		
		public void Initialize() {
			_garageScenario.Start();
		}
	}
}
