using Gameplay;
using TMPro;
using UnityEngine;

public class GameplayHudView : View, IFlyingTarget {
    [SerializeField] private string _stageCaption= "Stage";
    [SerializeField] private TMP_Text _stageNumberText;
    [SerializeField] private TMP_Text _mapNumberText;
    [SerializeField] private TMP_Text _coinsText;
    [SerializeField] private Transform _rewardTarget;

    public int stageNumber {
        set => _stageNumberText.text = $"{_stageCaption} {value}";
    }

    public Vector2Int mapNumber {
        set => _mapNumberText.text = $"{value.x}/{value.y}";
    }

    public bool isMapNumberVisible {
        set => _mapNumberText.enabled = value;
    }
    
    public string coinsText {
	    set => _coinsText.text = value;
        get => _coinsText.text;
    }

    public bool isCoinsVisible {
	    set => _coinsText.enabled = value;
    }

    public Transform rewardTargetTransform => _rewardTarget;
}