using Project;
using UnityEngine;

namespace Gameplay {

	public class FinalizeState : State {
		private readonly StateSwitcher _stateSwitcher;
		private readonly GameplayCache _gameplayCache;
		private readonly GameProcess _gameProcess;
		private readonly EnemyPointerSystem _enemyPointerSystem;
		private readonly ZombieStorage _zombieStorage;
		private readonly PauseService _pauseService;

		public FinalizeState(StateSwitcher stateSwitcher,
				GameplayCache gameplayCache,
				GameProcess gameProcess,
				EnemyPointerSystem enemyPointerSystem,
				ZombieStorage zombieStorage,
				PauseService pauseService) : base(stateSwitcher) {
			_stateSwitcher = stateSwitcher;
			_gameplayCache = gameplayCache;
			_gameProcess = gameProcess;
			_enemyPointerSystem = enemyPointerSystem;
			_zombieStorage = zombieStorage;
			_pauseService = pauseService;
		}

		public override void Enter() {
			_gameProcess.Finish();
			_enemyPointerSystem.Clear();
			_pauseService.Clear();
			
			DestroyGameObjects();
			
			SwitchToPrepare();
		}

		private void DestroyGameObjects() {
			_zombieStorage.DestroyAll();
			
			var car = _gameplayCache.car;
			var map = _gameplayCache.map;

			Object.Destroy(car.gameObject);
			Object.Destroy(map.gameObject);

			_gameplayCache.car = null;
			_gameplayCache.map = null;
		}

		private void SwitchToPrepare() =>
				_stateSwitcher.SetState<ConstructState>();

	}
}
