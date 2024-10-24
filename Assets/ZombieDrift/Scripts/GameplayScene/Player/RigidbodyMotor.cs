using UnityEngine;

public class RigidbodyMotor : MonoBehaviour {
    [SerializeField] private Rigidbody _rigidBody;
    public Rigidbody body => _rigidBody;
    public float maxSpeed { get; set; }
    public float acceleration { get; set; }
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
    public float steerTurn {
        get => _steerInput;
        set => _steerInput = value;
    }

    public float rigidbodyVelocity => _rigidBody.linearVelocity.magnitude;

    private float _steerInput, _accelerationInput;

    private void Awake() {
        _rigidBody.centerOfMass = centerOfMass;
    }

    public void FixedTick() {
        var moveVector = transform.forward * (acceleration);
        moveVector.y = 0;
        // _moveForceVector = moveVector; 
        if (_steerInput != 0) {
            var rotateVector = Vector3.up * (_steerInput * rigidbodyVelocity * steerAngle);
            transform.Rotate(rotateVector);
            //transform.Rotate(Vector3.up * (_steerInput * rigidbodyVelocity.magnitude * steerAngle));
        }

        if (rigidbodyVelocity < maxSpeed)
            _rigidBody.AddForce(moveVector, ForceMode.Acceleration);
        
    }
}