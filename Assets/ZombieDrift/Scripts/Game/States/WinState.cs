using Cysharp.Threading.Tasks;

public class WinState : State {
    private const int MAP_CHANGE_DELAY_MILLISECONDS = 2000;

    private readonly StateSwitcher _stateSwitcher;
    private readonly SaveLoadSystem _saveLoadSystem;
    private readonly GameCache _gameCache;
    private readonly MapClearPresenter _mapClearPresenter;
    private readonly StagesConfig _stagesConfig;
    private readonly CameraSystem _cameraSystem;
    private bool isStageComplete => _gameCache.mapIndex + 1 > _gameCache.mapsCount - 1;

    public WinState(StateSwitcher stateSwitcher,
        SaveLoadSystem saveLoadSystem,
        GameCache gameCache,
        MapClearPresenter mapClearPresenter,
        StagesConfig stagesConfig,
        CameraSystem cameraSystem) : base(stateSwitcher) {
        _stateSwitcher = stateSwitcher;
        _saveLoadSystem = saveLoadSystem;
        _gameCache = gameCache;
        _mapClearPresenter = mapClearPresenter;
        _stagesConfig = stagesConfig;
        _cameraSystem = cameraSystem;
    }

    public override async void Enter() {
        _cameraSystem.isZoomed = true;
        if (isStageComplete) {
            await ShowStageClearedWithDelay();
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
        _mapClearPresenter.enabled = true;
        _mapClearPresenter.isMapLabelEnabled = true;
        await UniTask.Delay(MAP_CHANGE_DELAY_MILLISECONDS);
        _mapClearPresenter.isMapLabelEnabled = false;
        _mapClearPresenter.enabled = false;
    }

    private async UniTask ShowStageClearedWithDelay() {
        _mapClearPresenter.enabled = true;
        _mapClearPresenter.isStageClearedEnabled = true;
        await UniTask.Delay(MAP_CHANGE_DELAY_MILLISECONDS);
        _mapClearPresenter.isStageClearedEnabled = false;
        _mapClearPresenter.enabled = false;
    }

    private void IncreaseStage() {
        _gameCache.stageIndex++;
        _gameCache.mapIndex = 0;
    }

    private void SwitchToPrepareState() {
        _stateSwitcher.SetState<FinalizeState>();
    }
}