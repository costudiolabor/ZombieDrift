using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriftCar : MonoBehaviour {
    [SerializeField] private Rigidbody rigidBody;
    public float MoveSpeed = 50;
    public float SteerAngle = 20;
    private Vector3 _moveForceVector;
    private float _steerInput, _accelerationInput;
    
    [SerializeField] private List<Wheels> axleInfos;
    [SerializeField] private float maxMotorTorque;
    [SerializeField] private float maxSteeringAngle;
    [SerializeField] private float maxBrake;
    
    private float _vertical;
    private float _horizontal;
    private bool _brake;

    public class Wheels {
        public WheelCollider leftWheel;
        public WheelCollider rightWheel;
        public bool motor; 
        public bool steering; 
        public bool brake; 
    }

    private IEnumerator Start() {
        Wheels wheels = axleInfos[0];
       
        //if (wheels.motor) {
            wheels.leftWheel.motorTorque = maxMotorTorque;
            wheels.rightWheel.motorTorque = maxMotorTorque;

            yield return null;
            wheels.leftWheel.motorTorque = 0;
            wheels.rightWheel.motorTorque = 0;
        //}
    }

    private void Update() {
        _steerInput = Input.GetAxis("Horizontal");
        _accelerationInput = Input.GetAxis("Vertical");
    }

    private void FixedUpdate() {
        WheelsUpdate();
        _moveForceVector = transform.forward * (MoveSpeed * _accelerationInput);
        if (_steerInput != 0)
            transform.Rotate(Vector3.up * (_steerInput * rigidBody.velocity.normalized.magnitude * SteerAngle));
        rigidBody.AddForce(_moveForceVector, ForceMode.Acceleration);
    }

    private void OnDrawGizmos() {
        Debug.DrawRay(transform.position, _moveForceVector.normalized * 3);
        Debug.DrawRay(transform.position, transform.forward * 3, Color.blue);
    }
    
    
     private void WheelsUpdate() {
        //float motor = maxMotorTorque * _accelerationInput;       
        float steering = maxSteeringAngle * _steerInput; 
        //foreach (AxleInfo axleInfo in axleInfos) {
        
        //front
        Wheels wheels = axleInfos[0];
            if (wheels.steering) {
                wheels.leftWheel.steerAngle = steering;
                wheels.rightWheel.steerAngle = steering;
            }

            // if (wheels.motor) {
            //   wheels.leftWheel.motorTorque = motor;
            //   wheels.rightWheel.motorTorque = motor;
            // }

            // if (wheels.brake) {
            //     if (_brake) {
            //         wheels.leftWheel.brakeTorque = maxBrake;
            //         wheels.rightWheel.brakeTorque = maxBrake;
            //     }
            //     else {
            //         wheels.leftWheel.brakeTorque = 0;
            //         wheels.rightWheel.brakeTorque = 0;
            //     }
            // }
            ApplyLocalPositionToVisuals(wheels.leftWheel);
            ApplyLocalPositionToVisuals(wheels.rightWheel);
            
            //rear
            // wheels = axleInfos[1];
            // if (wheels.steering) {
            //     wheels.leftWheel.steerAngle = -steering;
            //     wheels.rightWheel.steerAngle = -steering;
            // }
            //
            // if (wheels.motor) {
            //   wheels.leftWheel.motorTorque = motor;
            //   wheels.rightWheel.motorTorque = motor;
            // }
            //
            // if (wheels.brake) {
            //     if (_brake) {
            //         wheels.leftWheel.brakeTorque = maxBrake;
            //         wheels.rightWheel.brakeTorque = maxBrake;
            //     }
            //     else {
            //         wheels.leftWheel.brakeTorque = 0;
            //         wheels.rightWheel.brakeTorque = 0;
            //     }
            // }

            //_Rpm = axleInfo.rightWheel.rpm; // округление и перевод в int
            // ApplyLocalPositionToVisuals(wheels.leftWheel);
            // ApplyLocalPositionToVisuals(wheels.rightWheel);
       // }
    }
    
     
     private void ApplyLocalPositionToVisuals(WheelCollider collider) {
         if (collider.transform.childCount == 0) {
             return;
         }
         Transform visualWheel = collider.transform.GetChild(0);
         collider.GetWorldPose(out var position, out var rotation);
         var transform1 = visualWheel.transform;
         transform1.position = position;
         transform1.rotation = rotation;
     }
    
    
}
