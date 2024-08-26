using Project;
using UnityEngine;

namespace Gameplay {

	public class FinalizeState : State {
		private readonly StateSwitcher _stateSwitcher;
		private readonly GameplayCache _gameplayCache;
		private readonly GameProcess _gameProcess;
		private readonly EnemyPointerSystem _enemyPointerSystem;

		public FinalizeState(StateSwitcher stateSwitcher,
				GameplayCache gameplayCache,
				GameProcess gameProcess,
				EnemyPointerSystem enemyPointerSystem) : base(stateSwitcher) {
			_stateSwitcher = stateSwitcher;
			_gameplayCache = gameplayCache;
			_gameProcess = gameProcess;
			_enemyPointerSystem = enemyPointerSystem;
		}

		public override void Enter() {
			_gameProcess.Finish();
			_enemyPointerSystem.Clear();
			DestroyGameObjects();
			SwitchToPrepare();
		}

		public void DestroyGameObjects() {
			var zombies = _gameplayCache.zombies;
			var car = _gameplayCache.car;
			var map = _gameplayCache.map;

			foreach (var zombie in zombies)
				Object.Destroy(zombie.gameObject);

			Object.Destroy(car.gameObject);
			Object.Destroy(map.gameObject);

			_gameplayCache.zombies = null;
			_gameplayCache.car = null;
			_gameplayCache.map = null;
		}

		private void SwitchToPrepare() =>
				_stateSwitcher.SetState<ConstructState>();

	}
}
