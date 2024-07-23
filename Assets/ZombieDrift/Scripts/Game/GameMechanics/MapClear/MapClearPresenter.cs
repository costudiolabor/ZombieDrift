using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapClearPresenter 
{
   private readonly GameCache _gameCache;
   private MapClearView _view;

   public MapClearPresenter(GameCache gameCache) {
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

   public void Initialize(MapClearView mapClearView) {
      _view = mapClearView;
   }


}