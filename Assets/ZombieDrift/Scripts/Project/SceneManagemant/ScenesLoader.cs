using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Project {
	public class ScenesLoader {
		private FadeLoadingView _loading;

		public ScenesLoader(RootCanvas rootCanvas) =>
				_loading = rootCanvas.loading;

		public void SwitchToShopScene() =>
				SwitchSceneWithAnimation(SceneNames.GARAGE);

		public void SwitchToGameplayScene() =>
				SwitchSceneWithAnimation(SceneNames.GAMEPLAY);

		private async void SwitchSceneWithAnimation(string sceneName) {
			await _loading.FadeIn();

			await SwitchTo(SceneNames.BOOT);
			await SwitchTo(sceneName);

			_loading.FadeOut();
		}

		private async UniTask SwitchTo(string sceneName) {
			var switchSceneAsyncOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
			while (!switchSceneAsyncOperation.isDone)
				await UniTask.Yield();
		}
	}
}
