using UnityEngine;

public class RigidbodyMotor : MonoBehaviour {
    [SerializeField] private Rigidbody _rigidBody;
    public Rigidbody body => _rigidBody;
    public float maxSpeed { get; set; }
    public float moveSpeed { get; set; }
    public float steerAngle { get; set; }

    public float mass {
        get => _rigidBody.mass;
        set => _rigidBody.mass = value;
    }

    public float drag {
        get => _rigidBody.linearDamping;
        set => _rigidBody.linearDamping = value;
    }

    public float angularDrag {
        get => _rigidBody.linearDamping;
        set => _rigidBody.linearDamping = value;
    }

    public Vector3 centerOfMass {
        get => _rigidBody.centerOfMass;
        set => _rigidBody.centerOfMass = value;
    }

    /*public bool isRun {
        get => _accelerationInput > 0;
        set => _accelerationInput = value ? 1 : 0;
    }*/

    public float steerTurn {
        get => _steerInput;
        set => _steerInput = value;
    }

    public Vector3 rigidbodyVelocity => _rigidBody.linearVelocity;

    private GUIStyle _guiStyle;
    private Vector3 _moveForceVector;
    private float _steerInput, _accelerationInput;

    private void Awake() {
        _rigidBody.centerOfMass = centerOfMass;
    }

    public void FixedTick() {
        _moveForceVector = transform.forward * (moveSpeed);
        if (_steerInput != 0)
            transform.Rotate(Vector3.up * (_steerInput * rigidbodyVelocity.magnitude * steerAngle));

        if (rigidbodyVelocity.magnitude < maxSpeed)
            _rigidBody.AddForce(_moveForceVector, ForceMode.Acceleration);
    }
}