using System;
using UnityEngine;
namespace Gameplay {

	public interface IGameEvents {
		event Action<Vector3> ObstacleHitEvent;
		event Action<Zombie> ZombieHitEvent;
	}
}
