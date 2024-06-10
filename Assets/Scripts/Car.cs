using UnityEngine;

public class Car : MonoBehaviour {
    [SerializeField] private Rigidbody rigidBody;
    public float MoveSpeed = 50;
    public float SteerAngle = 20;
    private Vector3 _moveForceVector;
    private float _steerInput, _accelerationInput;

    private void Update() {
        _steerInput = Input.GetAxis("Horizontal");
        _accelerationInput = Input.GetAxis("Vertical");
    }

    private void FixedUpdate() {
        _moveForceVector = transform.forward * (MoveSpeed * _accelerationInput);
        if (_steerInput != 0)
            //transform.Rotate(Vector3.up * (_steerInput * rigidBody.linearVelocity.normalized.magnitude * SteerAngle));
            transform.Rotate(Vector3.up * (_steerInput * rigidBody.velocity.normalized.magnitude * SteerAngle));
        rigidBody.AddForce(_moveForceVector, ForceMode.Acceleration);
    }

    private void OnDrawGizmos() {
        Debug.DrawRay(transform.position, _moveForceVector.normalized * 3);
        Debug.DrawRay(transform.position, transform.forward * 3, Color.blue);
    }
}