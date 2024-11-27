using Ads;
using Project;
using SaveLoadSystemNamespace;

namespace Gameplay {
    public class LoseState : State {
        private readonly StateSwitcher _stateSwitcher;
        private readonly SaveLoadSystem _saveLoadSystem;
        private readonly GameplayCache _gameplayCache;
        private readonly LosePresenter _losePresenter;
        private readonly CameraSystem _cameraSystem;
        private readonly AdsSystem _adsSystem;
        private readonly Progress _progress;

        public LoseState(
            StateSwitcher stateSwitcher,
            SaveLoadSystem saveLoadSystem,
            GameplayCache gameplayCache,
            LosePresenter losePresenter,
            CameraSystem cameraSystem,
            AdsSystem adsSystem,
            Progress progress) : base(stateSwitcher) {
            _stateSwitcher = stateSwitcher;
            _saveLoadSystem = saveLoadSystem;
            _gameplayCache = gameplayCache;
            _losePresenter = losePresenter;
            _cameraSystem = cameraSystem;
            _adsSystem = adsSystem;
            _progress = progress;
        }

        public override void Enter() {
            ShowCameraActions();

            SaveGame();

            _losePresenter.enabled = true;
            _losePresenter.RepairEvent += OnRepairClicked;
            _losePresenter.RestartEvent += OnStartFromScratchClicked;
        }

        public override void Exit() {
            _losePresenter.enabled = false;
            _losePresenter.RepairEvent -= OnRepairClicked;
            _losePresenter.RestartEvent -= OnStartFromScratchClicked;
            _cameraSystem.isZoomed = false;
        }

        private void SaveGame() => 
            _saveLoadSystem.SaveObject(SaveType.PlayerPrefs, _progress);

        private async void ShowCameraActions() {
            await _cameraSystem.Shake(1, 250);
            _cameraSystem.isZoomed = true;
        }

        private async void OnRepairClicked() {
            _losePresenter.isRepairInteractable = false;

            var rewardCollected = await _adsSystem.ShowRewardVideo();

            if (rewardCollected)
                SwitchToRepairState();

            _losePresenter.isRepairInteractable = true;
        }

        private void OnStartFromScratchClicked() {
            _gameplayCache.mapIndex = 0;
            _adsSystem.ShowFullscreen();
            SwitchToRestartState();
        }

        private void SwitchToRestartState() =>
            _stateSwitcher.SetState<FinalizeState>();

        private void SwitchToRepairState() =>
            _stateSwitcher.SetState<RepairState>();
    }
}