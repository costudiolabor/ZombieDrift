using System;
using System.Collections.Generic;
using UnityEngine;

public class GameProcess {
    public event Action<Vector3> EnemyHitEvent, ObstacleHitEvent; 
    public event Action AllEnemiesDestroyedEvent; 
    
    private List<IDamageable> _damageableList;
    private Car _car;
  
    public void Initialize(GameplayData data) {
        _damageableList = new List<IDamageable>();
       
        foreach (var zombie in data.zombies)
            _damageableList.Add(zombie);

        _car = data.car;
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

    private void OnDamageableHit(IDamageable damageable) {
        _damageableList.Remove(damageable);
        EnemyHitEvent?.Invoke(damageable.position);
        
        if(_damageableList.Count == 0)
           AllEnemiesDestroyedEvent?.Invoke();    
    }
}