using Project;
using UnityEngine;

namespace Gameplay {

    public class FinalizeState : State {
        private readonly StateSwitcher _stateSwitcher;
        private readonly GameCache _gameCache;
        private readonly GameProcess _gameProcess;

        public FinalizeState(StateSwitcher stateSwitcher,
            GameCache gameCache,
            GameProcess gameProcess) : base(stateSwitcher) {
            _stateSwitcher = stateSwitcher;
            _gameCache = gameCache;
            _gameProcess = gameProcess;
        }

        public override void Enter() {
            _gameProcess.Finish();

            DestroyGameObjects();
            SwitchToPrepare();
        }

        public void DestroyGameObjects() {
            var zombies = _gameCache.zombies;
            var car = _gameCache.car;
            var map = _gameCache.map;

            foreach (var zombie in zombies)
                UnityEngine.Object.Destroy(zombie.gameObject);

            UnityEngine.Object.Destroy(car.gameObject);
            UnityEngine.Object.Destroy(map.gameObject);

            _gameCache.zombies = null;
            _gameCache.car = null;
            _gameCache.map = null;
        }

        private void SwitchToPrepare() {
            _stateSwitcher.SetState<ConstructState>();
        }
    }
}