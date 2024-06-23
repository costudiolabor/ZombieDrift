using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class ProjectInstaller : MonoInstaller {
    [FormerlySerializedAs("projectSettings")] [SerializeField] private ProjectConfig _projectConfig;
    [SerializeField] private RootCanvas rootCanvas;

    public override void InstallBindings() {
        InstallSceneLoader();
        InstallProgressService();
        CreateAndInstallRootCanvas();
        
        InstallProjectEntryPoint();
    }

    private void InstallProgressService() {
        Container.Bind<ProgressService>().AsSingle();
    }

    private void CreateAndInstallRootCanvas() {
        var canvas = Instantiate(rootCanvas, this.transform);
        Container.Bind<RootCanvas>().FromInstance(canvas);
    }

    private void InstallProjectEntryPoint() {
        Container.BindInterfacesAndSelfTo<ProjectEntryPoint>().AsSingle().NonLazy();
        Container.Bind<ProjectConfig>().FromInstance(_projectConfig).AsSingle();
    }

    private void InstallSceneLoader() {
        Container.Bind<ScenesLoader>().AsSingle();
    }
}