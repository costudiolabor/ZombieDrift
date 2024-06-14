using System;
using UnityEngine;
using UnityEngine.Serialization;

public class Wheels : MonoBehaviour {
   [SerializeField] private Car car;
   [SerializeField] private Transform[] wheelsFront;
   [SerializeField] private Transform[] wheelsRear;
   [SerializeField] private GameObject[] trails;
   [SerializeField] private ParticleSystem[] particles;

   [SerializeField] private float speedFrontWheel;
   [SerializeField] private float angleFrontWheel;
   [SerializeField] private float speedRearWheel;
   
   private Transform[] _visualWheels;
   private Vector2 _axis;
   private float _angle;
   private bool _isPlayParticles;
   

   private void Awake() {
      Initialize();
   }

   private void Initialize() {
      _visualWheels = new Transform[wheelsFront.Length];
      for (int i = 0; i < wheelsFront.Length; i++) {
         _visualWheels[i] = wheelsFront[i].GetChild(0);
      }
   }

   private void Update() {
      _axis = car.GetAxis();
      _angle = _axis.x * angleFrontWheel;

      foreach (var trail in trails) {
         //trail.SetActive(angle != 0);
      }
      
      foreach (var particle in particles) {
         //trail.SetActive(angle != 0);
      }
      
      for (int i = 0; i < wheelsFront.Length; i++) {
         wheelsFront[i].localEulerAngles = new Vector3(0, _angle, 0);
         _visualWheels[i].Rotate(Vector3.left, -_axis.y * speedFrontWheel);
      }
      
      foreach (var wheel in wheelsRear) {
         wheel.Rotate(Vector3.left, -_axis.y * speedRearWheel);
         if (_axis.y != 0) PlayParticles();
         else
            StopParticles();
      }
   }

   private void PlayParticles() {
      if (_isPlayParticles == true) return;
      foreach (var particle in particles) {
         particle.loop = true;
         particle.Play();
      }
      _isPlayParticles = true;
   }
   
   private void StopParticles() {
      if (_isPlayParticles == false) return;
      foreach (var particle in particles) {
         particle.loop = false;
      }
      _isPlayParticles = false;
   }
   
}
