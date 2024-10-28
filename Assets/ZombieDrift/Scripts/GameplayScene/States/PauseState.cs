using Project;
using UnityEngine;
using UnityEngine.Localization;

namespace Gameplay {
	public class PauseState : State {
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

		public PauseState(StateSwitcher stateSwitcher,
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
			Debug.Log("Pause");

			_levelCompletePresenter.ContinueEvent += SwitchToPrepareState;
		}

		public override void Exit() {
			_levelCompletePresenter.ContinueEvent -= SwitchToPrepareState;
		}

		private void SwitchToPrepareState() =>
				_stateSwitcher.SetState<FinalizeState>();
	}
}
