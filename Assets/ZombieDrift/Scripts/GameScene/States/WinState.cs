using Cysharp.Threading.Tasks;
using Project;
using UnityEngine;

namespace Gameplay {

    public class WinState : State {
        private const int MAP_CHANGE_DELAY_MILLISECONDS = 2000;

        private readonly StateSwitcher _stateSwitcher;
        private readonly SaveLoadSystem _saveLoadSystem;
        private readonly GameCache _gameCache;
        private readonly LevelComplete _levelComplete;
        private readonly StagesConfig _stagesConfig;
        private readonly CameraSystem _cameraSystem;
        private readonly ParticlesPlayer _particlesPlayer;
        private bool isStageComplete => _gameCache.mapIndex + 1 > _gameCache.mapsCount - 1;

        public WinState(StateSwitcher stateSwitcher,
            SaveLoadSystem saveLoadSystem,
            GameCache gameCache,
            LevelComplete levelComplete,
            StagesConfig stagesConfig,
            CameraSystem cameraSystem,
            ParticlesPlayer particlesPlayer) : base(stateSwitcher) {
            _stateSwitcher = stateSwitcher;
            _saveLoadSystem = saveLoadSystem;
            _gameCache = gameCache;
            _levelComplete = levelComplete;
            _stagesConfig = stagesConfig;
            _cameraSystem = cameraSystem;
            _particlesPlayer = particlesPlayer;
        }

        public override async void Enter() {
            _cameraSystem.isZoomed = true;
            if (isStageComplete) {
                
                await ShowClearedLabelWithDelay();
            //    _particlesPlayer.PlayWinConfetti(Vector3.zero);
                IncreaseStage();
                _saveLoadSystem.Save(_gameCache.saveData1);
            }
            else {
                await ShowMapClearedWithDelay();
                _gameCache.mapIndex++;
            }

            SwitchToPrepareState();
        }

        public override void Exit() {
            _cameraSystem.isZoomed = false;
        }

        private async UniTask ShowMapClearedWithDelay() {
            _levelComplete.enabled = true;
            _levelComplete.isMapLabelEnabled = true;
            await UniTask.Delay(MAP_CHANGE_DELAY_MILLISECONDS);
            _levelComplete.isMapLabelEnabled = false;
            _levelComplete.enabled = false;
        }

        private async UniTask ShowClearedLabelWithDelay() {
            _levelComplete.enabled = true;
            _levelComplete.isStageClearedEnabled = true;
            await UniTask.Delay(MAP_CHANGE_DELAY_MILLISECONDS);
            _levelComplete.isStageClearedEnabled = false;
            _levelComplete.enabled = false;
        }

        private void IncreaseStage() {
            _gameCache.stageIndex++;
            _gameCache.mapIndex = 0;
        }

        private void SwitchToPrepareState() {
            _stateSwitcher.SetState<FinalizeState>();
        }
    }
}