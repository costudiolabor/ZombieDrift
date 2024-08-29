using Unity.AI.Navigation;
using UnityEngine;
namespace Gameplay {

	public class Map : MonoBehaviour {
		private const float WALL_WIDTH = 1;
		[SerializeField] private NavMeshSurface _navMeshSurface;
		[SerializeField] private Transform _startPoint;
		[SerializeField] private Transform[] _zombieSpawnPointses;
#if UNITY_EDITOR

		[Header("Size")]
		[SerializeField] private float _mapHeight;

		[SerializeField] private float _mapWidth;

		[Header("Components")]
		[SerializeField] private Transform _wallNorth;

		[SerializeField] private Transform _wallSouth, _wallEast, _wallWest, _floor;
#endif

		public Transform[] zombieSpawnPoints => _zombieSpawnPointses;
		public Transform startPoint => _startPoint;

		public NavMeshSurface navMeshSurface => _navMeshSurface;


#if UNITY_EDITOR
		private void OnValidate1() {
			var halfWallWidth = WALL_WIDTH / 2;

			_wallSouth.localScale = new Vector3(WALL_WIDTH, 1, _mapHeight);
			_wallSouth.localPosition = new Vector3(_mapWidth / 2 - halfWallWidth, 0.5f, 0);

			_wallNorth.localScale = new Vector3(WALL_WIDTH, 1, _mapHeight);
			_wallNorth.localPosition = new Vector3(-1 * _mapWidth / 2 + halfWallWidth, 0.5f, 0);

			_wallEast.localScale = new Vector3(_mapWidth, 1, WALL_WIDTH);
			_wallEast.localPosition = new Vector3(0, 0.5f, _mapHeight / 2 - halfWallWidth);

			_wallWest.localScale = new Vector3(_mapWidth, 1, WALL_WIDTH);
			_wallWest.localPosition = new Vector3(0, 0.5f, -1 * _mapHeight / 2 + halfWallWidth);

			_floor.localScale = new Vector3(_mapWidth, 0.1f, _mapHeight);
		}
#endif
	}
}
