using UnityEngine;
using Zenject;

public class GameplayInstaller : MonoInstaller {
    [SerializeField] private GameEntryPoint _entryPoint;
    
    [SerializeField] private ZombiesConfig _zombiesConfig;
    [SerializeField] private StagesConfig _stagesConfig;
    [SerializeField] private CarsConfig _carsConfig;

    public override void InstallBindings() {
        InstallGameplayEntryPoint();
        InstallGameScenario();
        InstallConfigs();
        InstallFactory();
        InstallServices();
    }

    private void InstallServices() {
        Container.Bind<ContentCreationService>().AsSingle();
        Container.Bind<CameraSystem>().AsSingle();
    }

    private void InstallFactory() {
        Container.Bind<Factory>().AsSingle();
    }

    private void InstallGameplayEntryPoint() {
        Container.BindInterfacesAndSelfTo<GameEntryPoint>().FromInstance(_entryPoint);
    }

    private void InstallConfigs() {
        Container.Bind<ZombiesConfig>().FromInstance(_zombiesConfig);
        Container.Bind<CarsConfig>().FromInstance(_carsConfig);
        Container.Bind<StagesConfig>().FromInstance(_stagesConfig);
    }

    private void InstallGameScenario() {
        Container.Bind<GameScenario>().AsSingle();
        Container.Bind<StateSwitcher>().AsSingle();
        Container.Bind<PrepareMapState>().AsSingle();
        Container.Bind<GameState>().AsSingle();
    }
}