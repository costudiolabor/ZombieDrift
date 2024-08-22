using Unity.Cinemachine;
using UnityEngine;
using Zenject;

namespace Gameplay {
    public class EntryPoint : MonoBehaviour, IInitializable {
        [SerializeField] private CinemachineCamera _mainCamera;
        [SerializeField] private CinemachineCamera _zoomCamera;
        [SerializeField] private MainMenuView _mainMenuView;
        [SerializeField] private LoseView _loseView;
        [SerializeField] private StageView _stageView;
        [SerializeField] private GetReadyView _getReadyView;
        [SerializeField] private HowToPlayView _howToPlayView;
        [SerializeField] private MapClearedView _mapClearedView;
       
        [SerializeField] private PointersView _pointersView;
        [SerializeField] private Camera _camera;
        private GameplayScenario _gameplayScenario;

        [Inject]
        private void Construct(
            GameplayScenario gameplayScenario,
            CameraSystem cameraSystem,
            MainMenuPresenter mainMenuPresenter,
            LosePresenter losePresenter,
            StageLabelPresenter stageLabelPresenter,
            GetReadyPresenter getReadyPresenter,
            HowToPlayPresenter howToPlayPresenter,
            LevelComplete levelComplete,
            EnemyPointerSystem enemyPointerSystem) {
            cameraSystem.mainCamera = _mainCamera;
            cameraSystem.zoomCamera = _zoomCamera;
            mainMenuPresenter.Initialize(_mainMenuView);
            losePresenter.Initialize(_loseView);
            stageLabelPresenter.Initialize(_stageView);
            getReadyPresenter.Initialize(_getReadyView);
            howToPlayPresenter.Initialize(_howToPlayView);
            levelComplete.Initialize(_mapClearedView);
            enemyPointerSystem.Initialize(_pointersView, _camera);

            _gameplayScenario = gameplayScenario;
        }

        public void Initialize() =>
            _gameplayScenario.Start();
    }
}