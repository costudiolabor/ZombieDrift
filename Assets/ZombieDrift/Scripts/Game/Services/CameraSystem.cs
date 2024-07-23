using System;
using Cinemachine;
using Cysharp.Threading.Tasks;
using UnityEngine;


public class CameraSystem {

   public CinemachineVirtualCamera mainCamera {
      set {
         if (value == null)
            return;
         _mainCamera = value;
         _basicMultiChannelPerlin = _mainCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
      }
   }
   
   public CinemachineVirtualCamera zoomCamera {
      set {
         if (value == null)
            return;
         _zoomCamera = value;
      }
   }

   public bool isZoomed {
      get => _isZoomed;
      set {
         _mainCamera.enabled = !value;
         _zoomCamera.enabled = value;
      }
   }
   public Transform target {
      set {
         if (_mainCamera == null || _zoomCamera==null)
            throw new Exception($"Cinemachine camera is null, please set camera property in CameraSystem ");
         _mainCamera.Follow = value;
         _zoomCamera.Follow = value;
      }
   }

   private CinemachineBasicMultiChannelPerlin _basicMultiChannelPerlin;
   private CinemachineVirtualCamera _mainCamera, _zoomCamera;
   private bool _isZoomed;

   public async UniTask Shake(float amplitude, int shakeDurationMilliseconds) {
      _basicMultiChannelPerlin.m_AmplitudeGain = amplitude;
      await UniTask.Delay(shakeDurationMilliseconds);
      _basicMultiChannelPerlin.m_AmplitudeGain = 0;
   }
}