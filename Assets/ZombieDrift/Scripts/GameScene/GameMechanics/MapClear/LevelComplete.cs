using Project;

namespace Gameplay {
    public class LevelComplete {
        private readonly GameplayCache _gameplayCache;
        private MapClearedView _view;

        public LevelComplete(GameplayCache gameplayCache) {
            _gameplayCache = gameplayCache;
        }

        public bool enabled {
            set => _view.isVisible = value;
        }

        public bool isMapLabelEnabled {
            set {
                _view.mapText = $"Map {_gameplayCache.mapIndex + 1}/{_gameplayCache.mapsCount} cleared";
                _view.isMapTextEnabled = value;
            }
        }

        public bool isStageClearedEnabled {
            set => _view.isStageTextEnabled = value;
        }

        public void Initialize(MapClearedView mapClearedView) =>
            _view = mapClearedView;
    }
}