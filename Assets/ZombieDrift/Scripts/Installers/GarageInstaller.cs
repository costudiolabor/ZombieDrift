using Gameplay;
using Project;
using UnityEngine;
using Zenject;


namespace Garage {
    public class GarageInstaller : MonoInstaller {
        [SerializeField] private EntryPoint _entryPoint;
        [SerializeField] private CarsConfig _carsConfig;

        public override void InstallBindings() {
            InstallEntryPoint();
            InstallGarage();
            InstallStateMachine();

        }
        private void InstallStateMachine() {
	        Container.Bind<StateSwitcher>().AsSingle();
	        
	        Container.Bind<GarageScenario>().AsSingle();
	        Container.Bind<InitializeState>().AsSingle();
	        Container.Bind<SelectionState>().AsSingle();
        }

        private void InstallEntryPoint() =>
            Container.BindInterfacesAndSelfTo<EntryPoint>().FromInstance(_entryPoint);


        private void InstallGarage() {
	        Container.Bind<GarageItemsSwitcher>().AsSingle();
            Container.Bind<GaragePresenter1>().AsSingle();
            Container.Bind<CarsConfig>().FromInstance(_carsConfig);
        }
    }
}