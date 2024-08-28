using System;

namespace Gameplay {
    public class ComboSystem {
        private const float COMBO_DELAY = 0.9f;
            //  private const int MIN_COMBO_COUNT_FOR_NOTIFY = 2;

        //   public event Action<int> ComboHappenedEvent;

        private readonly Timer _timer;
        private readonly ComboCounter _comboCounter;

        public ComboSystem() {
            _comboCounter = new ComboCounter();
            _timer = new Timer();
            _timer.AlarmEvent += OnComboTimeSpend;
        }

        public int IncreaseCombo() {
            _comboCounter.Increase();
            _timer.StartWithAlarm(COMBO_DELAY);
            return _comboCounter.comboCount;
        }

        public void Tick() =>
            _timer.Tick();

        private void OnComboTimeSpend() =>
            _comboCounter.Reset();

        ~ComboSystem() =>
            _timer.AlarmEvent -= OnComboTimeSpend;
    }
}