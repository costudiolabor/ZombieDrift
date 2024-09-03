using Unity.AI.Navigation;
using UnityEngine;

namespace Gameplay {
    public class Map : MonoBehaviour {
        private const float WALL_WIDTH = 1;
        [SerializeField] private NavMeshSurface _navMeshSurface;
        [SerializeField] private Transform _startPoint;
        [SerializeField] private Transform[] _zombieSpawnPointses;

        public Transform[] zombieSpawnPoints => _zombieSpawnPointses;
        public Transform startPoint => _startPoint;
        public NavMeshSurface navMeshSurface => _navMeshSurface;
    }
}