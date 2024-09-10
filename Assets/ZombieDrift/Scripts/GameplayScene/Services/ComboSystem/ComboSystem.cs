using UnityEngine;
namespace Gameplay {
	public class ComboSystem {
		public float carMultiplier { get; set; }
		public int count => _count;

		private readonly Timer _timer;
		private readonly int _minComboCountForNotify;
		private readonly float _comboLifeTime;

		private int _count;

		public ComboSystem(ComboConfig comboConfig) {
			_timer = new Timer();
			_timer.AlarmEvent += Reset;

			_minComboCountForNotify = comboConfig.minComboCountForNotify;
			_comboLifeTime = comboConfig.comboLifeTime;
		}

		public int IncreaseAndTryGetReward() {
			var comboCount = IncreaseCombo();
			if (comboCount < _minComboCountForNotify)
				return 0;

			return Mathf.RoundToInt(comboCount * carMultiplier);
		}

		private int IncreaseCombo() {
			++_count;
			_timer.StartWithAlarm(_comboLifeTime);
			return _count;
		}

		public void TimerRefresh() =>
				_timer.Tick();

		public void Reset() =>
				_count = 0;

		~ComboSystem() =>
				_timer.AlarmEvent -= Reset;
	}
}
