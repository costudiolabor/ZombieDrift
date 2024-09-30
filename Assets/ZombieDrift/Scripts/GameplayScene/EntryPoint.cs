using Unity.Cinemachine;
using UnityEngine;
using Zenject;

namespace Gameplay {
    public class EntryPoint : MonoBehaviour, IInitializable {
        [SerializeField] private Camera _camera;
        [SerializeField] private CinemachineCamera _mainCamera;
        [SerializeField] private CinemachineCamera _zoomCamera;
        [SerializeField] private MainMenuView _mainMenuView;
        [SerializeField] private LoseView _loseView;
        [SerializeField] private GameplayHudView _gameHudView;
        [SerializeField] private GetReadyView _getReadyView;
        [SerializeField] private HowToPlayView _howToPlayView;
        [SerializeField] private WinView winView;

        [SerializeField] private PointersView _pointersView;
        private GameplayScenario _gameplayScenario;

        [Inject]
        private void Construct(
            GameplayScenario gameplayScenario,
            CameraSystem cameraSystem,
            MenuPresenter menuPresenter,
            LosePresenter losePresenter,
            GameplayHud gameplayHud,
            GetReadyPresenter getReadyPresenter,
            HowToPlayPresenter howToPlayPresenter,
            LevelCompletePresenter levelCompletePresenter,
            EnemyPointerSystem enemyPointerSystem,
            TextHintSystem textHintSystem,
            FlyingRewardSystem flyingRewardSystem) {
            cameraSystem.mainCamera = _mainCamera;
            cameraSystem.zoomCamera = _zoomCamera;
            menuPresenter.Initialize(_mainMenuView);
            losePresenter.Initialize(_loseView);
            gameplayHud.Initialize(_gameHudView);
            getReadyPresenter.Initialize(_getReadyView);
            howToPlayPresenter.Initialize(_howToPlayView);
            levelCompletePresenter.Initialize(winView);

            enemyPointerSystem.Initialize(_pointersView, _camera);
            flyingRewardSystem.Initialize(_camera, _gameHudView.rewardTargetTransform);
            textHintSystem.Initialize(_camera);

            _gameplayScenario = gameplayScenario;
        }

        public void Initialize() =>
            _gameplayScenario.Start();
    }
}