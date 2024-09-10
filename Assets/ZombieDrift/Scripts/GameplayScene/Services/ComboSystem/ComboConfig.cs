using UnityEngine;

[CreateAssetMenu(fileName = "ComboConfig", menuName = "Configs/ComboConfig")]
public class ComboConfig : ScriptableObject {
	[SerializeField] private int _minComboCountForNotify = 2;
	[SerializeField] private int _comboMultiplier = 5;
	[SerializeField] private float _comboLifeTime = 0.9f;

	public int minComboCountForNotify => _minComboCountForNotify;
	public int comboMultiplier => _comboMultiplier;
	public float comboLifeTime => _comboLifeTime;
}
