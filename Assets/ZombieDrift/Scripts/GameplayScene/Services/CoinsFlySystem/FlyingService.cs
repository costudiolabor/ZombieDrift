using System;
using UnityEngine;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Project;
using Random = UnityEngine.Random;

namespace Gameplay {
	public class FlyingService {
		public event Action CollectedEvent, AllCollectedEvent;
		public Vector3 destination { get; set; }

		private readonly FlyingCoinsConfig _config;
		private readonly List<FlyingElement> _flyingList = new();
		private PoolObjects<FlyingElement> _flyingItemsPool;

		public FlyingService(FlyingCoinsConfig config) {
			_config = config;
			var parent = new GameObject(_config.itemsParentName).transform;
			_flyingItemsPool = new PoolObjects<FlyingElement>(_config.prefab, _config.poolAmount, true, parent);
		}

		public void SpawnInSphere(Vector3 sphereCenter, int count) {
			for (var i = 0; i < count; i++) {
				var randomPointInSphere = Random.insideUnitSphere * _config.radius + sphereCenter + _config.offsetInSphere;
				CreateWithDelays(randomPointInSphere);
			}
		}

		public void SpawnInCircle(Vector2 circleCenter, int count) {
			for (var i = 0; i < count; i++) {
				var randomPointInCircle = Random.insideUnitCircle * _config.radius + circleCenter + _config.offsetInCircle;
				CreateWithDelays(randomPointInCircle);
			}
		}

		private async void CreateWithDelays(Vector3 position) {
			await UniTask.Delay(_config.delayBeforeAppear);
			var coin = _flyingItemsPool.GetFreeElement();
			coin.position = position;

			AddToFlyingListWithDelay(coin);
		}

		private async void AddToFlyingListWithDelay(FlyingElement coinTransform) {
			await UniTask.Delay(_config.delayBeforeStartMoving);
			_flyingList.Add(coinTransform);
		}

		public void FlyByFixedTick() {
			if (_flyingList.Count <= 0)
				return;

			for (var i = _flyingList.Count - 1; i >= 0; i--) {
				_flyingList[i].position = Vector3.MoveTowards(_flyingList[i].position, destination, _config.coinSpeed);

				if (_flyingList[i].position != destination)
					continue;

				_flyingList[i].isActive = false;
				_flyingList.RemoveAt(i);

				CollectedEvent?.Invoke();

				if (_flyingList.Count == 0)
					AllCollectedEvent?.Invoke();
			}
		}
	}
}
