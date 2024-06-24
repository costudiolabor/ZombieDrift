using System;
using UnityEngine;

public class Car : MonoBehaviour {
   public event Action<IDamageable> HitDamageableEvent;
   public event Action<Vector3> CarDestroyedEvent;

   [SerializeField] private RigidbodyMotor _motor;
   [SerializeField] private float maxSpeed = 15;
   [SerializeField] private float moveSpeed = 40;
   [SerializeField] private float steerAngle = 0.25f;
   [SerializeField] private float mass = 1000;
   [SerializeField] private float drag = 0;
   [SerializeField] private float angularDrag = 0.02f;
   [SerializeField] private Vector3 centerOfMass = new(0, -0.2f, 0);
   public RigidbodyMotor motor => _motor;

   public void Initialize() {
      _motor.maxSpeed = maxSpeed;
      _motor.moveSpeed = moveSpeed;
      _motor.steerAngle = steerAngle;
      _motor.centerOfMass = centerOfMass;
      _motor.drag = drag;
      _motor.angularDrag = angularDrag;
      _motor.mass = mass;
   }

   private void OnTriggerEnter(Collider other) {
      var damageable = other.GetComponent<IDamageable>();
      if (damageable == null) return;
      damageable?.Damage();
      HitDamageableEvent?.Invoke(damageable); ;
   }

   private void OnCollisionEnter(Collision other) {
      var obstacle = other.gameObject.GetComponent<IObstacle>();
      
      if(obstacle == null)
         return;
      
      CarDestroyedEvent?.Invoke(other.contacts[0].point);
   }
}