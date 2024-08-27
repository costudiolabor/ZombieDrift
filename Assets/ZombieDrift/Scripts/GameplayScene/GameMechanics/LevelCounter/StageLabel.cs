using System;
using UnityEngine;

namespace Gameplay {
    public enum StagePresentState {
        None,
        StageOnly,
        All
    }

    public class StageLabel {
        private StageView _view;

        public void Initialize(StageView stageView) {
            _view = stageView;
        }

        public int stageIndex {
            set => _view.stageNumber = value + 1;
        }

        public Vector2Int mapIndex {
            set => _view.mapNumber = new Vector2Int(value.x + 1, value.y);
        }

        public int moneyCount {
	        set => _view.coinsText = value.ToString();
        }

        public StagePresentState presentState {
            set {
                switch (value) {
                    case StagePresentState.None:
                        _view.isVisible = false;
                        break;
                    case StagePresentState.StageOnly:
                        _view.isVisible = true;
                        _view.isMapNumberVisible = false;
                        break;
                    case StagePresentState.All:
                        _view.isVisible = true;
                        _view.isMapNumberVisible = true;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(value), value, null);
                }
            }
        }
    }
}