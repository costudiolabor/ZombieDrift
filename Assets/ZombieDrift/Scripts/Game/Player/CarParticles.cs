using System;
using UnityEngine;

[Serializable]
public class CarParticles {

    [SerializeField] private ParticleSystem[] _wheelsSmoke;
    [SerializeField] private ParticleSystem _crashParticles;

    public bool isWheelSmokeEnabled {
        set {
            foreach (var smoke in _wheelsSmoke)
                if (value)
                    smoke.Play();
                else
                    smoke.Stop();
        }
    }

    public bool isCrashSmokeEnabled {
        set {
            if (value)
                _crashParticles.Play();
            else
                _crashParticles.Stop();
        }
    }
}