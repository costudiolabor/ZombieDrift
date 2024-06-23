using System;
using UnityEngine;

public class RigidbodyMotor : MonoBehaviour {
    [SerializeField] private Rigidbody rigidBody;
    public float maxSpeed { get; set; }
    public float moveSpeed { get; set; }
    public float steerAngle { get; set; }

    public float mass {
        get => rigidBody.mass;
        set => rigidBody.mass = value;
    }

    public float drag {
        get => rigidBody.drag;
        set => rigidBody.drag = value;
    }

    public float angularDrag {
        get => rigidBody.drag;
        set => rigidBody.drag = value;
    }

    public Vector3 centerOfMass {
        get => rigidBody.centerOfMass;
        set => rigidBody.centerOfMass = value;
    }

    public Vector3 rigidbodyVelocity => rigidBody.velocity;

    private GUIStyle _guiStyle;
    private Vector3 _moveForceVector;
    private float _steerInput, _accelerationInput;

    private void Awake() {
        rigidBody.centerOfMass = centerOfMass;
    }

    private void Update() {
        _steerInput = Input.GetAxis("Horizontal");
        _accelerationInput = Input.GetAxis("Vertical");
    }

    private void FixedUpdate() {
        _moveForceVector = transform.forward * (moveSpeed * _accelerationInput);
        if (_steerInput != 0)
            transform.Rotate(Vector3.up * (_steerInput * rigidbodyVelocity.magnitude * steerAngle));

        if (rigidbodyVelocity.magnitude < maxSpeed)
            rigidBody.AddForce(_moveForceVector, ForceMode.Acceleration);
    }

    private void OnTriggerEnter(Collider other) {
        var activeObstacle = other.GetComponent<IDamagable>();
        activeObstacle?.Damage();
    }

#if UNITY_EDITOR
    private void OnGUI() {
        _guiStyle = new GUIStyle {
            fontSize = 20,
            normal = new GUIStyleState() { textColor = Color.green },
        };
        GUI.Label(new Rect(Screen.width / 2, Screen.height / 2, 100, 50), $"Speed {rigidBody.velocity.magnitude:F}",
            _guiStyle);
    }
#endif
}