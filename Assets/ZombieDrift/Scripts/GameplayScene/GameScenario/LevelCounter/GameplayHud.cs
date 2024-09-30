using System;
using UnityEngine;
using UnityEngine.Localization;

namespace Gameplay {
    public enum StagePresentState {
        None,
        StageOnly,
        All
    }

    public class GameplayHud  {
        private GameplayHudView _view;
        
        private int _moneyCount;
        
        public void Initialize(GameplayHudView gameplayHudView) {
	        _view = gameplayHudView;
        }

        public int stageIndex {
            set => _view.stageNumber = value + 1;
        }

        public Vector2Int mapIndex {
            set => _view.mapNumber = new Vector2Int(value.x + 1, value.y);
        }

        public int moneyCount {
            set {
                _view.coinsText = value.ToString();
                _moneyCount = value;
            }
            get => _moneyCount;
        }
        public StagePresentState presentState {
            set {
                switch (value) {
                    case StagePresentState.None:
                        _view.isActive = false;
                        break;
                    case StagePresentState.StageOnly:
                        _view.isActive = true;
                        _view.isMapNumberVisible = false;
                        break;
                    case StagePresentState.All:
                        _view.isActive = true;
                        _view.isMapNumberVisible = true;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(value), value, null);
                }
            }
        }

    }
}