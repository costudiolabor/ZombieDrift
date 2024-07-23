using System;
using System.Collections.Generic;
using UnityEngine;

public class GameProcess : IGameEvents {
    public event Action<Vector3> ObstacleHitEvent;
    public event Action<Zombie> ZombieHitEvent;
    public event Action AllEnemiesDestroyedEvent;

    private List<Zombie> _damageableList;
    private Car _car;

    public void Initialize(Car car, Zombie[] zombies) {
        _damageableList = new List<Zombie>(zombies);
        SetCar(car);
    }

    public void SetCar(Car car) {
        _car = car;
        _car.HitDamageableEvent += OnDamageableHit;
        _car.CarDestroyedEvent += OnObstacleHit;
    }

    public void Finish() {
        _car.HitDamageableEvent -= OnDamageableHit;
        _car.CarDestroyedEvent -= OnObstacleHit;
        _car = null;
        _damageableList = null;
    }

    private void OnObstacleHit(Vector3 obj) {
        ObstacleHitEvent?.Invoke(obj);
    }

    private void OnDamageableHit(Zombie damageable) {
        _damageableList.Remove(damageable);
        ZombieHitEvent?.Invoke(damageable);

        if (_damageableList.Count == 0)
            AllEnemiesDestroyedEvent?.Invoke();
    }
}