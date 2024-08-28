public class ComboCounter {
	public int comboCount => _comboCount;
	
	private int _comboCount;

	public int Increase() =>
			++_comboCount;

	public void Reset() =>
			_comboCount = 0;
}
