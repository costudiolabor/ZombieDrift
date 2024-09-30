using UnityEngine;
namespace Gameplay {
	public class ComboSystem {
		private const int MIN_COMBO_COUNT_FOR_NOTIFY = 2;

		public float comboMultiplier { get; set; }
		public float comboDelay { get; set; }
		public int count => _count;

		private readonly Timer _timer;

		private int _count;

		public ComboSystem() {
			_timer = new Timer();
			_timer.AlarmEvent += Reset;
		}

		public int IncreaseAndTryGetReward() {
			var comboCount = IncreaseCombo();

			return comboCount >= MIN_COMBO_COUNT_FOR_NOTIFY
					? Mathf.RoundToInt(comboCount * comboMultiplier)
					: 0;
		}

		private int IncreaseCombo() {
			++_count;
			_timer.StartWithAlarm(comboDelay);
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
