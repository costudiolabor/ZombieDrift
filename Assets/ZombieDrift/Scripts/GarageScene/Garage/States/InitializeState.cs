using System.Linq;
using Gameplay;
using Project;
using UnityEngine;

namespace Garage {
	public class InitializeState : State {
		private readonly StateSwitcher _stateSwitcher;
		private readonly CarsConfig _carsConfig;
		private readonly ProjectCache _projectCache;
		private readonly GaragePresenter1 _garagePresenter;
		public InitializeState(
				StateSwitcher stateSwitcher,
				CarsConfig carsConfig,
				ProjectCache projectCache,
				GaragePresenter1 garagePresenter) : base(stateSwitcher) {
			_stateSwitcher = stateSwitcher;
			_carsConfig = carsConfig;
			_projectCache = projectCache;
			_garagePresenter = garagePresenter;
		}

		public override void Enter() {
			CreateAndHideAllCarModels();
			SwitchToSelectionState();
		}

		private void CreateAndHideAllCarModels() {
			var carsCount = _carsConfig.count;
			var configCarsArray = _carsConfig.cars;

			for (var i = 0; i < carsCount; i++) {
				var carModel = Object.Instantiate(configCarsArray[i].car.mesh, _garagePresenter.spawnPoint);
				carModel.SetActive(false);

				var isCarIsPurchased = _projectCache.purchasedCars.Contains(i);
				if (!isCarIsPurchased)
					LockCar(carModel);

				var garageData = new GarageItem() {
						mesh = carModel,
						price = configCarsArray[i].price,
//						isPurchased = isCarIsPurchased 
				};
				_garagePresenter.AddData(garageData);
			}
		}
		private void LockCar(GameObject car) =>
				Utils.MoveAllChildrenToLayer(car.transform, _carsConfig.shopNotPurchasedLayerMask);
		private void SwitchToSelectionState() {
			_stateSwitcher.SetState<SelectionState>();
		}
	}
}
