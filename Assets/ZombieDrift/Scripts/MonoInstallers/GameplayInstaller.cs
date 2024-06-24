using UnityEngine;
using Zenject;

public class GameplayInstaller : MonoInstaller {
    [SerializeField] private GameEntryPoint _entryPoint;
    
    [SerializeField] private ZombiesConfig _zombiesConfig;
    [SerializeField] private StagesConfig _stagesConfig;
    [SerializeField] private CarsConfig _carsConfig;
    [SerializeField] private InputConfig _inputConfig;

    public override void InstallBindings() {
        InstallGameplayEntryPoint();
        InstallGameScenario();
        InstallConfigs();
        InstallFactory();
        InstallServices();
        InstallGameplay();
        InstallInput();
    }

    private void InstallInput() {
        Container.Bind<ITickable>().To<IInput>().FromResolve();
      
        if (Application.isMobilePlatform)
            InstallTouchInput();
        else
            InstallKeyboardInput();
    }

    private void InstallKeyboardInput() { 
        Container.Bind<InputConfig>().FromInstance(_inputConfig).AsSingle();
        Container.Bind<IInput>().To<KeyboardInput>().AsSingle();
    }

    private void InstallTouchInput() {
        Container.Bind<IInput>().To<TouchInput>().AsSingle();
    }

    private void InstallGameplay() {
        Container.Bind<GameProcess>().AsSingle();
        Container.Bind<VehicleController>().AsSingle();
    }

    private void InstallServices() {
        Container.Bind<ContentCreationService>().AsSingle();
        Container.Bind<CameraSystem>().AsSingle();
        Container.Bind<PauseService>().AsSingle();
        Container.Bind<PlayCache>().AsSingle();
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
        Container.Bind<ConstructState>().AsSingle();
        Container.Bind<PlayState>().AsSingle();
        Container.Bind<WinState>().AsSingle();
        Container.Bind<LoseState>().AsSingle();
        Container.Bind<RestartState>().AsSingle();
        Container.Bind<FinalizeState>().AsSingle();
    }
}