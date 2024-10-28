using System;
using Project;
using UnityEngine;
using UnityEngine.Localization;

namespace Gameplay {
    public enum StagePresentState {
        None,
        StageOnly,
        All
    }

    public class GameplayHud  {
	    private readonly UiSoundsPlayer _uiSoundsPlayer;
	    private GameplayHudView _view;
        
        private int _moneyCount;

        public GameplayHud(UiSoundsPlayer uiSoundsPlayer) {
	        _uiSoundsPlayer = uiSoundsPlayer;
        }
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
	        /*get {
		     
		        return _moneyCount;
	        }*/
        }

        public void IncreaseMoneyCount(int count = 1) {
	        moneyCount = _moneyCount + count;
	        _uiSoundsPlayer.PlayCoinSound();
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