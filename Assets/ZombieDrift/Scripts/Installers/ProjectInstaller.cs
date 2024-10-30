using UnityEngine;
using Zenject;

namespace Project {
	public class ProjectInstaller : MonoInstaller {
		[SerializeField] private ProjectConfig _projectConfig;
		[SerializeField] private RootCanvas rootCanvas;
		[SerializeField] private UiSoundConfig _uiSoundConfig;

		public override void InstallBindings() {
			InstallSceneLoader();
			InstallProgressService();
			CreateAndInstallRootCanvas();
			InstallProjectEntryPoint();
			InstallCache();
			InstallUiSounds();
		}
		private void InstallUiSounds() {
			Container.Bind<UiSounds>().AsSingle();
			Container.Bind<UiSoundConfig>().FromInstance(_uiSoundConfig);
		}
		private void InstallCache() {
			Container.Bind<ProjectCache>().AsSingle();
		}

		private void InstallProgressService() {
			Container.Bind<SaveLoadSystem>().AsSingle();
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
}
