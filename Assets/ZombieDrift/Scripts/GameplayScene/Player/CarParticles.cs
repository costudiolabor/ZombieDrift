using System;
using UnityEngine;

[Serializable]
public class CarParticles {

    [SerializeField] private ParticleSystem[] _wheelsSmoke;
    [SerializeField] private ParticleSystem[] _crashParticles;
    
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
            foreach (var crash in _crashParticles)
                if (value)
                    crash.Play();
                else
                    crash.Stop();
        }
    }
}