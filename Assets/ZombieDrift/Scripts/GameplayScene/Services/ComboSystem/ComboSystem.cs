namespace Gameplay {
    public class ComboSystem {
        public float comboDelay { get; set; } = 0.9f;
        private readonly Timer _timer;
        private int _comboCount;

        public ComboSystem() {
            _timer = new Timer();
            _timer.AlarmEvent += Reset;
        }

        public int IncreaseCombo() {
            ++_comboCount;
            _timer.StartWithAlarm(comboDelay);
            return _comboCount;
        }

        public void Tick() =>
            _timer.Tick();

        private void Reset() =>
            _comboCount = 0;

        ~ComboSystem() =>
            _timer.AlarmEvent -= Reset;
    }
}