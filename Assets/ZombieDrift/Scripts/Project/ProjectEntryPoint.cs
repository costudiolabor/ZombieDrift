using Ads;
using Cysharp.Threading.Tasks;
using GamePush;
using GamePush.Initialization;
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
        private readonly AdsSystem _adsSystem;

        public ProjectEntryPoint(
            ScenesLoader scenesLoader,
            ProjectConfig config,
            Progress progress,
            UiSounds uiSounds,
            SaveLoadSystem saveLoadSystem,
            AdsSystem adsSystem) {
            _scenesLoader = scenesLoader;
            _progress = progress;
            _uiSounds = uiSounds;
            _saveLoadSystem = saveLoadSystem;
            _adsSystem = adsSystem;
            _config = config;
        }

        public void Initialize() =>
            Run();

        public async void Run() {
#if UNITY_WEBG
#endif
            GP_Initialization.Execute();
            _adsSystem.type = AdsType.GamePush;

            await LoadSavedData();
            SetUpProject();
            CreateLog();
            await SetSystemLocale();
            InitializeUiSounds();

            TurnOnStickyBanner();
            SwitchToGameplayScene();
        }

        private void TurnOnStickyBanner() {
            _adsSystem.ShowStickyBanner();
        }

        private void InitializeUiSounds() {
            var soundsParent = new GameObject(POOL_SOUNDS_PARENT_NAME).transform;
            Object.DontDestroyOnLoad(soundsParent);
            _uiSounds.Initialize(soundsParent);
        }

        private async UniTask SetSystemLocale() {
            await LocalizationSettings.InitializationOperation;

            //  SystemLanguage language = Application.systemLanguage;
            SystemLanguage language = GP_Language.CurrentSystemLanguage();
           Debug.Log("System lang "+language);
            var localeIdentifier = new LocaleIdentifier(language);
            Debug.Log("LocaleIdentifier"+localeIdentifier);
            //  var localeIdentifier = new LocaleIdentifier("tr-TR");
            //	var localeIdentifier = new LocaleIdentifier(SystemLanguage.Turkish);
            var f = LocalizationSettings.AvailableLocales.GetLocale(localeIdentifier);
            Debug.Log("Locale "+f);
            LocalizationSettings.SelectedLocale = f;
           
        }

        private async UniTask LoadSavedData() =>
#if !UNITY_EDITOR && UNITY_WEBGL
				await _saveLoadSystem.RestoreObject(SaveType.GamePushCloud, _progress);
#else
            await _saveLoadSystem.RestoreObject(SaveType.PlayerPrefs, _progress);
#endif

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