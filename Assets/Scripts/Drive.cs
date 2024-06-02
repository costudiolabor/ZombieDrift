using System;
using UnityEngine;

[Serializable]
public class Drive {
    [SerializeField] private Transform car;
    [SerializeField] private Rigidbody rigidBodyCar;
    [SerializeField] private float moveSpeed = 50;
    [SerializeField] private float Speed = 1.0f;
    [SerializeField] private float maxSpeed = 15;
    [SerializeField] private float drag = 0.98f;
    [SerializeField] private float steerAngle = 20;
    [SerializeField] private float traction = 1;
    private Vector3 _moveForce;

    public void UpdateDrive(Vector2 axis) {
        Move(axis.y);
        Steering(axis.x);
        Drag();
        //ShowDrawRay();
    }

    private void Move(float moveInput) {
        _moveForce += car.forward * moveSpeed * moveInput * Time.deltaTime;
        Vector3 position = car.position;
        position += _moveForce * Time.deltaTime;
        car.position = position;
        //rigidBodyCar.linearVelocity = new Vector3(0,0,_moveForce.z);
    } 
    
    // private void Move2(float moveInput) {
    //     var _movementVector = new Vector3(0.0f, 0.0f, moveInput);
    //     rigidBodyCar.AddForce(_movementVector * Speed, ForceMode.Impulse);
    // }
    //  private void Move4(float moveInput) {
    //     rigidBodyCar.linearVelocity = new Vector3(0,0,moveInput * Speed);
    // }
    //
    // private void Move3(float moveInput) {
    //    _moveForce += car.forward * moveSpeed * moveInput * Time.deltaTime;
    //     Vector3 movement = car.position;
    //     movement += _moveForce * Time.deltaTime;
    //     car.Translate(movement);
    // }

    private void Steering(float steerInput) {
        car.Rotate(Vector3.up * steerInput * _moveForce.magnitude * steerAngle * Time.deltaTime);
        //rigidBodyCar.AddTorque();
    }
    
    private void Drag() {
        _moveForce *= drag;
        _moveForce = Vector3.ClampMagnitude(_moveForce, maxSpeed);
        _moveForce = Vector3.Lerp(_moveForce.normalized, car.forward, traction * Time.deltaTime) * _moveForce.magnitude;
    }

    public void StopCar() {
        rigidBodyCar.isKinematic = true;
    }

    // private void ShowDrawRay() {
    //     Vector3 forward = car.forward;
    //     Vector3 position = car.position;
    //     Debug.DrawRay(position, _moveForce.normalized * 3);
    //     Debug.DrawRay(position, forward * 3, Color.blue);
    // }
}
