using Cinemachine;
using UnityEngine;
using Zenject;

public class GameEntryPoint : MonoBehaviour, IInitializable {
    [SerializeField] private CinemachineVirtualCamera _mainCamera;
    [SerializeField] private CinemachineVirtualCamera _zoomCamera;
    [SerializeField] private MainMenuView _mainMenuView;
    [SerializeField] private LoseView _loseView;
    [SerializeField] private StageView _stageView;
    [SerializeField] private GetReadyView _getReadyView;
    [SerializeField] private HowToPlayView _howToPlayView;
    [SerializeField] private MapClearView _mapClearView;
    private GameScenario _gameScenario;

    [Inject]
    private void Construct(
        GameScenario gameScenario,
        CameraSystem cameraSystem,
        MainMenuPresenter mainMenuPresenter,
        LosePresenter losePresenter,
        StagePresenter stagePresenter,
        GetReadyPresenter getReadyPresenter,
        HowToPlayPresenter howToPlayPresenter,
        MapClearPresenter mapClearPresenter) {
        
        cameraSystem.mainCamera = _mainCamera;
        cameraSystem.zoomCamera = _zoomCamera;
        mainMenuPresenter.Initialize(_mainMenuView);
        losePresenter.Initialize(_loseView);
        stagePresenter.Initialize(_stageView);
        getReadyPresenter.Initialize(_getReadyView);
        howToPlayPresenter.Initialize(_howToPlayView);
        mapClearPresenter.Initialize(_mapClearView);

        _gameScenario = gameScenario;
    }

    public void Initialize() =>
        _gameScenario.Start();
}