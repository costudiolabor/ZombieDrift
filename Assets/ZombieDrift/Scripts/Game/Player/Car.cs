using System;
using UnityEngine;

public class Car : MonoBehaviour {
   [SerializeField] private RigidbodyMotor _motor;
   [SerializeField] private float maxSpeed = 15;
   [SerializeField] private float moveSpeed = 40;
   [SerializeField] private float steerAngle = 0.25f;
   [SerializeField] private float mass = 1000;
   [SerializeField] private float drag = 0;
   [SerializeField] private float angularDrag = 0.02f;
   [SerializeField] private Vector3 centerOfMass = new(0, -0.2f, 0);
   private void Awake() {
      Initialize();
   }

   private void Initialize() {
      _motor.maxSpeed = maxSpeed;
      _motor.moveSpeed = moveSpeed;
      _motor.steerAngle = steerAngle;
      _motor.centerOfMass = centerOfMass;
      _motor.drag = drag;
      _motor.angularDrag = angularDrag;
      _motor.mass = mass;
   }
}
