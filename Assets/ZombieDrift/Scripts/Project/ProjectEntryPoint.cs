using UnityEngine;
using Zenject;

namespace Project {
	public class ProjectEntryPoint : IInitializable {
		private readonly ScenesLoader _scenesLoader;
		private readonly ProjectConfig _config;
		private readonly SaveLoadSystem _saveLoadSystem;
		private readonly ProjectCache _projectCache;

		public ProjectEntryPoint(
				ScenesLoader scenesLoader,
				ProjectConfig config,
				SaveLoadSystem saveLoadSystem,
				ProjectCache projectCache) {
			_scenesLoader = scenesLoader;
			_saveLoadSystem = saveLoadSystem;
			_projectCache = projectCache;
			_config = config;
		}

		public void Initialize() =>
				Run();

		public void Run() {
			LoadSavedData();
			SetUpProject();
			CreateLog();
			SwitchToGameplayScene();
		}

		private void LoadSavedData() =>
				_projectCache.saveData = _saveLoadSystem.Load();


		private void SetUpProject() =>
				Application.targetFrameRate = _config.targetFramerate;


		private void CreateLog() {
			if (_config.isFpsEnabled) {
				var fpsCounter = Object.Instantiate(_config.fpsCounterPrefab);
				Object.DontDestroyOnLoad(fpsCounter);
			}

			if (_config.isDebugEnable) {
				var guiLog = Object.Instantiate(_config.guiLogPrefab);
				Object.DontDestroyOnLoad(guiLog);
			}
		}

		private void SwitchToGameplayScene() =>
				_scenesLoader.SwitchToGameplayScene();

	}
}
