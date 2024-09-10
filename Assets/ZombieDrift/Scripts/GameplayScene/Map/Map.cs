using UnityEditor;
using UnityEngine;
using Unity.AI.Navigation;
using System.Collections.Generic;

namespace Gameplay {
	public class Map : MonoBehaviour {
		#if UNITY_EDITOR
		[SerializeField] private Transform _zombieSpawnPointsParent;
		#endif
		[SerializeField] private NavMeshSurface _navMeshSurface;
		[SerializeField] private Transform _startPoint;
		[SerializeField] private Transform[] _zombieSpawnPointses;

		public Transform[] zombieSpawnPoints => _zombieSpawnPointses;
		public Transform startPoint => _startPoint;
		public NavMeshSurface navMeshSurface => _navMeshSurface;

		#if UNITY_EDITOR
		public void UpdateZombiePoints() {
			if (EditorApplication.isPlaying)
				return;

			var zombiePoints = new List<Transform>();
			foreach (Transform child in _zombieSpawnPointsParent)
				zombiePoints.Add(child);

			_zombieSpawnPointses = zombiePoints.ToArray();
		}
		#endif
	}
}
