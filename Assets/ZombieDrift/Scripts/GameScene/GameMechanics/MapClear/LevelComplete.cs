using Project;

namespace Gameplay {
    public class LevelComplete {
        private readonly GameCache _gameCache;
        private MapClearedView _view;

        public LevelComplete(GameCache gameCache) {
            _gameCache = gameCache;
        }

        public bool enabled {
            set => _view.isVisible = value;
        }

        public bool isMapLabelEnabled {
            set {
                _view.mapText = $"Map {_gameCache.mapIndex + 1}/{_gameCache.mapsCount} cleared";
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