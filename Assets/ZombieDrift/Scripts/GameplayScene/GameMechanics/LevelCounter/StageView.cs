using TMPro;
using UnityEngine;

public class StageView : View {
    [SerializeField] private string _stageCaption= "Stage";
    [SerializeField] private TMP_Text _stageNumberText;
    [SerializeField] private TMP_Text _mapNumberText;
    [SerializeField] private TMP_Text _coinsText;

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
    }

    public bool isCoinsVisible {
	    set => _coinsText.enabled = value;
    }
}