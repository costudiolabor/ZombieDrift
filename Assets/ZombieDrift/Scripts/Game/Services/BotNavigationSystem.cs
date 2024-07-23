using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class BotNavigation : ITickable {
    private const float POSITION_REFRESH_RATE = 0.1f;
    private const float REACT_DISTANCE = 15;

    public Transform target { get; set; }

    private List<Zombie> _zombies;
    private float _totalSeconds;
    private bool _isRunning;

    public void Initialize(IEnumerable<Zombie> zombies, Transform targetTransform) {
        _zombies = zombies.ToList();
        target = targetTransform;
    }

    public void Start() {
        _isRunning = true;

      
    }

    public void Stop() {
        _isRunning = false;
    }

    public void Tick() {
        if (!_isRunning) return;
        _totalSeconds += Time.deltaTime;
        if (_totalSeconds < POSITION_REFRESH_RATE)
            return;
        _totalSeconds = 0;
        RefreshPosition();
    }

    public void RemoveKilledZombie(Zombie killedZombie) {
        _zombies.Remove(killedZombie);
    }

    private void RefreshPosition() {
        foreach (var zombie in _zombies) {
            var d = Vector3.Distance(target.position, zombie.position);
            if (d < REACT_DISTANCE)
                zombie.destination = target.position;
            /*else
                zombie.isRunning = false;*/
        }
    }
}