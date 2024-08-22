using Gameplay;
using UnityEngine;
using Zenject;


namespace Shop {
    public class ShopInstaller : MonoInstaller {
        [SerializeField] private EntryPoint _entryPoint;
        [SerializeField] private CarsConfig _carsConfig;

        public override void InstallBindings() {
            InstallEntryPoint();
            InstallShop();
            
        }

        private void InstallEntryPoint() =>
            Container.BindInterfacesAndSelfTo<EntryPoint>().FromInstance(_entryPoint);


        private void InstallShop() {
            Container.Bind<ShopPresenter>().AsSingle();
            Container.Bind<CarsConfig>().FromInstance(_carsConfig);
        }
    }
}