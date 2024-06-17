using UnityEngine;
using System;

[Serializable]
public class VFXCar {
    [SerializeField] private ParticleSystem[] particles;
    private bool _isPlayParticles;

    public void PlayParticles() {
        if (_isPlayParticles == true) return;
        foreach (var particle in particles) {
            particle.loop = true;
            particle.Play();
        }
        _isPlayParticles = true;
    }

    public void StopParticles() {
        if (_isPlayParticles == false) return;
        foreach (var particle in particles) {
            particle.loop = false;
        }
        _isPlayParticles = false;
    }
}
