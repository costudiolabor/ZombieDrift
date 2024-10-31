using Cysharp.Threading.Tasks;
using SaveLoadSystemNamespace;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using Zenject;

namespace Project {
	public class ProjectEntryPoint : IInitializable {
		private const string POOL_SOUNDS_PARENT_NAME = "SoundsParent";

		private readonly ScenesLoader _scenesLoader;
		private readonly ProjectConfig _config;
		private readonly Progress _progress;
		private readonly UiSounds _uiSounds;
		private readonly SaveLoadSystem _saveLoadSystem;

		public ProjectEntryPoint(
				ScenesLoader scenesLoader,
				ProjectConfig config,
				Progress progress,
				UiSounds uiSounds,
				SaveLoadSystem saveLoadSystem) {
			_scenesLoader = scenesLoader;
			_progress = progress;
			_uiSounds = uiSounds;
			_saveLoadSystem = saveLoadSystem;
			_config = config;
		}

		public void Initialize() =>
				Run();

		public async void Run() {
			LoadSavedData();
			SetUpProject();
			CreateLog();
			await SetSystemLocale();
			InitializeUiSounds();

			SwitchToGameplayScene();
		}
		private void InitializeUiSounds() {
			var soundsParent = new GameObject(POOL_SOUNDS_PARENT_NAME).transform;
			Object.DontDestroyOnLoad(soundsParent);
			_uiSounds.Initialize(soundsParent);
		}

		private async UniTask SetSystemLocale() {
			await LocalizationSettings.InitializationOperation;

			var language = Application.systemLanguage;
			var localeIdentifier = new LocaleIdentifier(language);

			LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.GetLocale(localeIdentifier);
		}

		private void LoadSavedData() =>
				_saveLoadSystem.RestoreObject(SaveType.PlayerPrefs, _progress);
		
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
