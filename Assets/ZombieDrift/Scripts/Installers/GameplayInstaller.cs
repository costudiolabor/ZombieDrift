using Project;
using UnityEngine;
using Zenject;

namespace Gameplay {
    public class GameplayInstaller : MonoInstaller {
        [SerializeField] private EntryPoint _entryPoint;

        [SerializeField] private ZombiesConfig _zombiesConfig;
        [SerializeField] private StagesConfig _stagesConfig;
        [SerializeField] private CarsConfig _carsConfig;
        [SerializeField] private InputConfig _inputConfig;
        [SerializeField] private ParticlesConfig _particlesConfig;
        [SerializeField] private FlyingRewardConfig _flyingRewardConfig;
        [SerializeField] private TextHintConfig _textHintConfig;

        public override void InstallBindings() {
            InstallGameplayEntryPoint();
            InstallGameScenario();
            InstallConfigs();
            InstallFactory();
            InstallServices();
            InstallGameplay();
            InstallInput();
            InstallGameplayCache();
        }

        private void InstallGameplayEntryPoint() =>
            Container.BindInterfacesAndSelfTo<EntryPoint>().FromInstance(_entryPoint);

        private void InstallGameplayCache() =>
            Container.Bind<GameplayCache>().AsSingle();

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

        private void InstallTouchInput() =>
            Container.Bind<IInput>().To<TouchInput>().AsSingle();

        private void InstallGameplay() {
            Container.Bind<MenuPresenter>().AsSingle();
            Container.Bind<LosePresenter>().AsSingle();
            Container.Bind<GameplayHud>().AsSingle();
            Container.Bind<GetReadyPresenter>().AsSingle();
            Container.Bind<HowToPlayPresenter>().AsSingle();
            Container.Bind<LevelComplete>().AsSingle();
            Container.Bind<GameProcess>().AsSingle();
            Container.Bind<VehicleController>().AsSingle();
            Container.Bind<VehicleDestroyer>().AsSingle();
            Container.BindInterfacesAndSelfTo<BotNavigation>().AsSingle();
            Container.Bind<EnemyPointerSystem>().AsSingle();
            Container.Bind<ParticlesPlayer>().AsSingle();
            Container.Bind<ParticlesConfig>().FromInstance(_particlesConfig);
        }

        private void InstallServices() {
            Container.Bind<ContentCreationService>().AsSingle();
            Container.Bind<CameraSystem>().AsSingle();
            Container.Bind<PauseService>().AsSingle();
            Container.Bind<MoneyWallet>().AsSingle();

            Container.BindInterfacesAndSelfTo<FlyingRewardSystem>().AsSingle().NonLazy();
            Container.Bind<FlyingRewardConfig>().FromInstance(_flyingRewardConfig);

            Container.Bind<ComboSystem>().AsSingle();

            Container.Bind<TextHintSystem>().AsSingle();
            Container.Bind<TextHintConfig>().FromInstance(_textHintConfig);
        }

        private void InstallFactory() =>
            Container.Bind<Factory>().AsSingle();

        private void InstallConfigs() {
            Container.Bind<ZombiesConfig>().FromInstance(_zombiesConfig);
            Container.Bind<CarsConfig>().FromInstance(_carsConfig);
            Container.Bind<StagesConfig>().FromInstance(_stagesConfig);
        }

        private void InstallGameScenario() {
            Container.Bind<GameplayScenario>().AsSingle();
            Container.BindInterfacesAndSelfTo<StateSwitcher>().AsSingle();
            Container.Bind<ConstructState>().AsSingle();
            Container.Bind<MenuState>().AsSingle();
            Container.Bind<GetReadyState>().AsSingle();
            Container.Bind<GameplayState>().AsSingle();
            Container.Bind<WinState>().AsSingle();
            Container.Bind<LoseState>().AsSingle();
            Container.Bind<RepairState>().AsSingle();
            Container.Bind<FinalizeState>().AsSingle();
        }
    }
}