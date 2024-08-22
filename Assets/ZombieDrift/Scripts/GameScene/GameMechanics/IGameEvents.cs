using System;
using UnityEngine;

public interface IGameEvents{
    event Action<Vector3>  ObstacleHitEvent;
    event Action<Zombie> ZombieHitEvent;
}