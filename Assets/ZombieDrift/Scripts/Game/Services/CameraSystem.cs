using System;
using Cinemachine;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class CameraSystem {

   public CinemachineVirtualCamera camera {
      set {
         if (value == null)
            return;
         _camera = value;
         _basicMultiChannelPerlin = _camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
      }
   }

   public Transform target {
      set {
         if (_camera == null)
            throw new Exception($"Cinemachine camera is null, please set camera property in CameraSystem ");
         _camera.Follow = value;
      }
   }

   private CinemachineBasicMultiChannelPerlin _basicMultiChannelPerlin;
   private CinemachineVirtualCamera _camera;

   public async void Shake(float amplitude, int shakeDurationMilliseconds) {
      _basicMultiChannelPerlin.m_AmplitudeGain = amplitude;
      await UniTask.Delay(shakeDurationMilliseconds);
      _basicMultiChannelPerlin.m_AmplitudeGain = 0;
   }
}