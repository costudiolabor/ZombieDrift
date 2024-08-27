public class ComboCounter {
	public int comboCountCount => _comboCount;
	
	private int _comboCount;

	public int Increase() =>
			++_comboCount;

	public void Reset() =>
			_comboCount = 0;
}
