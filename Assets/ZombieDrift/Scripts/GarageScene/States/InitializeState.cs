using System.Collections.Generic;
using Gameplay;
using Project;
using UnityEngine;

namespace Garage {
	public class InitializeState : State {
		private readonly StateSwitcher _stateSwitcher;
		private readonly CarsConfig _carsConfig;
		private readonly ProjectCache _projectCache;
		private readonly Podium _podium;
		private readonly ItemsSwitcher _itemsSwitcher;


		public InitializeState(
				StateSwitcher stateSwitcher,
				CarsConfig carsConfig,
				ProjectCache projectCache,
				Podium podium,
				ItemsSwitcher itemsSwitcher) : base(stateSwitcher) {
			_stateSwitcher = stateSwitcher;
			_carsConfig = carsConfig;
			_projectCache = projectCache;
			_podium = podium;
			_itemsSwitcher = itemsSwitcher;
		}

		public override void Enter() {
			CreateAllModels();
			SwitchToSelectionState();
		}

		private void CreateAllModels() {
			var configCarsArray = _carsConfig.cars;
			var carsCount = configCarsArray.Length;

			var garageItemList = new List<GarageItem>();

			for (var i = 0; i < carsCount; i++) {
				var carModel = Object.Instantiate(configCarsArray[i].car.mesh, _podium.spawnParent);
				carModel.SetActive(false);

				var isCarIsPurchased = _projectCache.purchasedCars.Contains(i);

				if (!isCarIsPurchased)
					LockCar(carModel);

				var garageData = new GarageItem() {
						mesh = carModel,
						price = configCarsArray[i].price,
						comboDelay = configCarsArray[i].comboDelay,
						comboMultiplier = configCarsArray[i].comboMultiplier
				};
				garageItemList.Add(garageData);
			}

			_itemsSwitcher.SetData(garageItemList.ToArray());
		}

		private void LockCar(GameObject car) =>
				Utils.MoveAllChildrenToLayer(car.transform, _carsConfig.lockedLayerMask);

		private void SwitchToSelectionState() =>
				_stateSwitcher.SetState<GarageState>();
	}
}
