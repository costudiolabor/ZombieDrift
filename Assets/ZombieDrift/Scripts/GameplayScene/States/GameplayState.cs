using UnityEngine;
using Project;

namespace Gameplay {
    public class GameplayState : State {
        private const float SHAKE_AMPLITUDE = 0.5f;
        private const int SHAKE_DURATION = 85;
        
        private const int MIN_COMBO_COUNT_FOR_NOTIFY = 2;
        private const int COMBO_MULTIPLIER = 5;
        private const float COMBO_ACTIVE_TIME = 0.9f;

        private readonly StateSwitcher _stateSwitcher;
        private readonly GameplayHud _gameplayHud;
        private readonly GameProcess _gameProcess;
        private readonly PauseService _pauseService;
        private readonly VehicleController _vehicleController;
        private readonly VehicleDestroyer _vehicleDestroyer;
        private readonly CameraSystem _cameraSystem;
        private readonly BotNavigation _botNavigation;
        private readonly ParticlesPlayer _particlesPlayer;
        private readonly EnemyPointerSystem _enemyPointerSystem;
        private readonly MoneyWallet _moneyWallet;
        private readonly FlyingRewardSystem _flyingRewardSystem;
        private readonly ComboSystem _comboSystem;
        private readonly TextHintSystem _textHintSystem;

        public GameplayState(StateSwitcher stateSwitcher,
            GameplayHud gameplayHud,
            GameProcess gameProcess,
            PauseService pauseService,
            VehicleController vehicleController,
            VehicleDestroyer vehicleDestroyer,
            CameraSystem cameraSystem,
            BotNavigation botNavigation,
            ParticlesPlayer particlesPlayer,
            EnemyPointerSystem enemyPointerSystem,
            MoneyWallet moneyWallet,
            FlyingRewardSystem flyingRewardSystem,
            ComboSystem comboSystem,
            TextHintSystem textHintSystem) : base(stateSwitcher) {
            _stateSwitcher = stateSwitcher;
            _gameplayHud = gameplayHud;
            _gameProcess = gameProcess;
            _pauseService = pauseService;
            _vehicleController = vehicleController;
            _vehicleDestroyer = vehicleDestroyer;
            _cameraSystem = cameraSystem;
            _botNavigation = botNavigation;
            _particlesPlayer = particlesPlayer;
            _enemyPointerSystem = enemyPointerSystem;
            _moneyWallet = moneyWallet;
            _flyingRewardSystem = flyingRewardSystem;
            _comboSystem = comboSystem;
            _textHintSystem = textHintSystem;
        }

        public override void Enter() {
            _gameplayHud.presentState = StagePresentState.All;
            _pauseService.SetPause(false);

            _vehicleController.Start();
            _botNavigation.Start();
            _enemyPointerSystem.enabled = true;

            _comboSystem.comboDelay = COMBO_ACTIVE_TIME;
            _flyingRewardSystem.CollectedEvent += OnFlyingRewardArrived;
           
            _gameProcess.ObstacleHitEvent += OnCarHitObstacle;
            _gameProcess.AllEnemiesDestroyedEvent += SwitchToWinState;
            _gameProcess.ZombieHitEvent += OnEnemyHit;
        }

        public override void Exit() {
            _gameplayHud.presentState = StagePresentState.None;
            _pauseService.SetPause(true);

            _vehicleController.Stop();
            _botNavigation.Stop();
            _enemyPointerSystem.enabled = false;

            _flyingRewardSystem.CollectedEvent -= OnFlyingRewardArrived;
            _gameProcess.ObstacleHitEvent -= OnCarHitObstacle;
            _gameProcess.AllEnemiesDestroyedEvent -= SwitchToWinState;
            _gameProcess.ZombieHitEvent -= OnEnemyHit;
        }

        public override void FixedTick() {
            _botNavigation.Tick();
            _enemyPointerSystem.Tick();
            _comboSystem.Tick();
        }

        private async void OnEnemyHit(Zombie zombie) {
            _particlesPlayer.PlayZombieHit(zombie.position);
            _botNavigation.RemoveKilledZombie(zombie);
            _enemyPointerSystem.Remove(zombie);

            _moneyWallet.AddCoins();
            _flyingRewardSystem.SpawnInSphere(zombie.position, 1);

            TryGetComboReward(zombie.position);

            await _cameraSystem.Shake(SHAKE_AMPLITUDE, SHAKE_DURATION);
        }

        private void OnCarHitObstacle(Vector3 point) {
            _particlesPlayer.PlayObstacleHit(point);
            _vehicleDestroyer.DestroyFormPoint(point);
            SwitchToLoseState();
        }

        private void TryGetComboReward(Vector3 hitPosition) {
            var comboCount = _comboSystem.IncreaseCombo();
            if (comboCount < MIN_COMBO_COUNT_FOR_NOTIFY)
                return;
            var rewardAmount = comboCount * COMBO_MULTIPLIER;
            _flyingRewardSystem.SpawnInSphere(hitPosition, rewardAmount);
            _moneyWallet.AddCoins(rewardAmount);
            _textHintSystem.ShowHint(hitPosition, $"Комбо x{comboCount}");
        }

        private void OnFlyingRewardArrived() =>
            _gameplayHud.moneyCount++;

        private void SwitchToWinState() =>
            _stateSwitcher.SetState<WinState>();

        private void SwitchToLoseState() =>
            _stateSwitcher.SetState<LoseState>();
    }
}