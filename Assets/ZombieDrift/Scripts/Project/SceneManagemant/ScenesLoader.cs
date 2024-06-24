using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesLoader {
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