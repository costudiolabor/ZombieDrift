using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesLoader {
    private const int FADE_IN_MILLISECONDS = 1000;

    private GameObject _loading;

    public ScenesLoader(RootCanvas rootCanvas) {
        _loading = rootCanvas.loading;
    }

    public void SwitchToBootScene() =>
        SwitchSceneWithAnimation(SceneNames.BOOT);

    public void SwitchToGameplayScene() =>
        SwitchSceneWithAnimation(SceneNames.GAMEPLAY);

    private async void SwitchSceneWithAnimation(string sceneName) {
        _loading.SetActive(true);
        await UniTask.Delay(FADE_IN_MILLISECONDS);

        await SwitchTo(SceneNames.BOOT);
        await SwitchTo(sceneName);

        _loading.SetActive(false);
    }

    private async UniTask SwitchTo(string sceneName) {
        var switchSceneAsyncOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        while (!switchSceneAsyncOperation.isDone)
            await UniTask.Yield();
    }
}