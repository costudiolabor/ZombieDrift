using System;
using UnityEngine;

public class RigidbodyMotor : MonoBehaviour {
    [SerializeField] private Rigidbody _rigidBody;
    public float maxSpeed { get; set; }
    public float moveSpeed { get; set; }
    public float steerAngle { get; set; }

    public float mass {
        get => _rigidBody.mass;
        set => _rigidBody.mass = value;
    }

    public float drag {
        get => _rigidBody.drag;
        set => _rigidBody.drag = value;
    }

    public float angularDrag {
        get => _rigidBody.drag;
        set => _rigidBody.drag = value;
    }

    public Vector3 centerOfMass {
        get => _rigidBody.centerOfMass;
        set => _rigidBody.centerOfMass = value;
    }

    public bool isRun {
        get => _accelerationInput > 0;
        set => _accelerationInput = value ? 1 : 0;
    }

    public float steerTurn {
        get => _steerInput;
        set => _steerInput = value;
    } 

    public Vector3 rigidbodyVelocity => _rigidBody.velocity;

    private GUIStyle _guiStyle;
    private Vector3 _moveForceVector;
    private float _steerInput, _accelerationInput;

    private void Awake() {
        _rigidBody.centerOfMass = centerOfMass;
    }
    
    private void FixedUpdate() {
        _moveForceVector = transform.forward * (moveSpeed * _accelerationInput);
        if (_steerInput != 0)
            transform.Rotate(Vector3.up * (_steerInput * rigidbodyVelocity.magnitude * steerAngle));

        if (rigidbodyVelocity.magnitude < maxSpeed)
            _rigidBody.AddForce(_moveForceVector, ForceMode.Acceleration);
    }
    
#if UNITY_EDITOR
    private void OnGUI() {
        _guiStyle = new GUIStyle {
            fontSize = 20,
            normal = new GUIStyleState() { textColor = Color.green },
        };
        GUI.Label(new Rect(Screen.width / 2, Screen.height / 2, 100, 50), $"Speed {_rigidBody.velocity.magnitude:F}",
            _guiStyle);
    }
#endif
}