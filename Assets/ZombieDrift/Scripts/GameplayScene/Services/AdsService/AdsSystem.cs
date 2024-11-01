using System;
using Cysharp.Threading.Tasks;
namespace Ads {

	public enum AdsType {
		GamePush = 0,
		Yandex = 1
	}
	public class AdsSystem : IAdsStrategy {
		public bool isFullscreenAvailable => _currentStrategy.isFullscreenAvailable;
		public bool isRewardedAvailable => _currentStrategy.isFullscreenAvailable;

		private readonly IAdsStrategy _gamePush = new GamePushStrategy();
		private IAdsStrategy _currentStrategy;

		public AdsType type {
			set {
				_currentStrategy = value switch {
						AdsType.GamePush => _gamePush,
						_ => throw new ArgumentOutOfRangeException(nameof(value), value, null)
				};
			}
		}

		public UniTask<bool> ShowFullscreen() {
			return _currentStrategy.ShowFullscreen();
		}
		public UniTask<bool> ShowReward() {
			return _currentStrategy.ShowFullscreen();
		}
	}
}
