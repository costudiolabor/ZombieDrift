using Cysharp.Threading.Tasks;
using Project;
using UnityEngine.Localization;

namespace Gameplay {
	public class WinState : State {
		private const int MAP_CHANGE_DELAY_MILLISECONDS = 2000;

		private const string LOCALIZE_TABLE = "StringsTable";
		private const string MAP_CLEARED_LOCAL_KEY = "mapClearedKey";
		private const string STAGE_CLEARED_KEY = "clearedKey";

		private readonly LocalizedString _mapClearedLocalizedString;
		private readonly LocalizedString _stageClearedLocalizedString;

		private readonly StateSwitcher _stateSwitcher;
		private readonly SaveLoadSystem _saveLoadSystem;
		private readonly ProjectCache _projectCache;
		private readonly GameplayCache _gameplayCache;
		private readonly LevelCompletePresenter _levelCompletePresenter;
		private readonly CameraSystem _cameraSystem;

		private bool isStageComplete => _gameplayCache.mapIndex + 1 > _gameplayCache.mapsCount - 1;

		public WinState(StateSwitcher stateSwitcher,
				SaveLoadSystem saveLoadSystem,
				ProjectCache projectCache,
				GameplayCache gameplayCache,
				LevelCompletePresenter levelCompletePresenter,
				CameraSystem cameraSystem
		) : base(stateSwitcher) {
			_stateSwitcher = stateSwitcher;
			_saveLoadSystem = saveLoadSystem;
			_projectCache = projectCache;
			_gameplayCache = gameplayCache;
			_levelCompletePresenter = levelCompletePresenter;
			_cameraSystem = cameraSystem;

			_mapClearedLocalizedString = new LocalizedString(LOCALIZE_TABLE, MAP_CLEARED_LOCAL_KEY);
			_stageClearedLocalizedString = new LocalizedString(LOCALIZE_TABLE, STAGE_CLEARED_KEY);
		}

		public override void Enter() {
			_cameraSystem.isZoomed = true;
			_levelCompletePresenter.ContinueEvent += SwitchToPrepareState;
			var winMessage = isStageComplete
					? _levelCompletePresenter.completeMessage = _stageClearedLocalizedString.GetLocalizedString()
					: _mapClearedLocalizedString.GetLocalizedString(_gameplayCache.mapIndex + 1, _gameplayCache.mapsCount);

			if (isStageComplete) {
				IncreaseStage();
				_saveLoadSystem.Save(_projectCache.saveData);
			}
			else {
				_gameplayCache.mapIndex++;
			}

			ShowClearedView(winMessage);
		}

		public override void Exit() {
			_levelCompletePresenter.ContinueEvent -= SwitchToPrepareState;
			_cameraSystem.isZoomed = false;
			CloseClearedView();
		}

		private void ShowClearedView(string message) {
			_levelCompletePresenter.completeMessage = message;
			_levelCompletePresenter.enabled = true;
		}

		private void CloseClearedView() {
			_levelCompletePresenter.enabled = false;
		}
		private void IncreaseStage() {
			_projectCache.stageIndex++;
			_gameplayCache.mapIndex = 0;
		}
		private void SwitchToPrepareState() =>
				_stateSwitcher.SetState<FinalizeState>();
	}
}
