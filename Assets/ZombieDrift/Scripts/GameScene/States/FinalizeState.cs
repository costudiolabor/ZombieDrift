using Project;
using UnityEngine;

namespace Gameplay {

	public class FinalizeState : State {
		private readonly StateSwitcher _stateSwitcher;
		private readonly GameCache _gameCache;
		private readonly GameProcess _gameProcess;
		private readonly EnemyPointerSystem _enemyPointerSystem;

		public FinalizeState(StateSwitcher stateSwitcher,
				GameCache gameCache,
				GameProcess gameProcess,
				EnemyPointerSystem enemyPointerSystem) : base(stateSwitcher) {
			_stateSwitcher = stateSwitcher;
			_gameCache = gameCache;
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
			var zombies = _gameCache.zombies;
			var car = _gameCache.car;
			var map = _gameCache.map;

			foreach (var zombie in zombies)
				Object.Destroy(zombie.gameObject);

			Object.Destroy(car.gameObject);
			Object.Destroy(map.gameObject);

			_gameCache.zombies = null;
			_gameCache.car = null;
			_gameCache.map = null;
		}

		private void SwitchToPrepare() =>
				_stateSwitcher.SetState<ConstructState>();

	}
}
