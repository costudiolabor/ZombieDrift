using Cinemachine;
using UnityEngine;
using Zenject;

public class GameEntryPoint : MonoBehaviour, IInitializable {
    [SerializeField] private CinemachineVirtualCamera _virtualCamera;
    private GameScenario _gameScenario;

    [Inject]
    private void Construct(GameScenario gameScenario, CameraSystem cameraSystem) {
        cameraSystem.camera = _virtualCamera;
        _gameScenario = gameScenario;
    }

    public void Initialize() {
        _gameScenario.Start();
    }
}