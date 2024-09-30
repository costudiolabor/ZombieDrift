using Gameplay;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;

public class GameplayHudView : View, IFlyingTarget {
	[SerializeField] private TMP_Text _stageNumberText;
	[SerializeField] private TMP_Text _mapNumberText;
	[SerializeField] private TMP_Text _coinsText;
	[SerializeField] private Transform _rewardTarget;
	[SerializeField] private LocalizedString _stageCaption;

	private int _stageNumber;

	public int stageNumber {
		set {
			_stageNumber = value;
			PrintStageNumber(_stageCaption.GetLocalizedString());
		}
	}
	private void PrintStageNumber(string stageCaption) {
		_stageNumberText.text = $"{stageCaption} {_stageNumber}";
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

	private void OnEnable() =>
			_stageCaption.StringChanged += PrintStageNumber;

	private void OnDisable() =>
			_stageCaption.StringChanged -= PrintStageNumber;
}
