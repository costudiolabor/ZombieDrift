using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class Wheels {
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;
    public bool motor; 
    public bool steering; 
    public bool brake; 
}

public class CarController : MonoBehaviour {
    [SerializeField] private List<Wheels> axleInfos;
    [SerializeField] private Rigidbody rigidBody;
    [SerializeField] private float maxMotorTorque;
    [SerializeField] private float maxSteeringAngle;
    [SerializeField] private float maxBrake;
    [SerializeField] private float driftFactor = 0.9f; 
    [SerializeField] private float normalFactor = 1.0f; 
    
    private float _vertical;
    private float _horizontal;
    private bool _brake;
   //bool isDrifting = false; 

    private void Awake() { Screen.sleepTimeout = SleepTimeout.NeverSleep; }
  
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

    private void FixedUpdate() {
        float motor = maxMotorTorque * _vertical;       
        float steering = maxSteeringAngle * _horizontal; 
        //foreach (AxleInfo axleInfo in axleInfos) {
        
        //front
        Wheels wheels = axleInfos[0];
            if (wheels.steering) {
                wheels.leftWheel.steerAngle = steering;
                wheels.rightWheel.steerAngle = steering;
            }

            if (wheels.motor) {
                wheels.leftWheel.motorTorque = motor;
                wheels.rightWheel.motorTorque = motor;
            }

            if (wheels.brake) {
                if (_brake) {
                    wheels.leftWheel.brakeTorque = maxBrake;
                    wheels.rightWheel.brakeTorque = maxBrake;
                }
                else {
                    wheels.leftWheel.brakeTorque = 0;
                    wheels.rightWheel.brakeTorque = 0;
                }
            }
            ApplyLocalPositionToVisuals(wheels.leftWheel);
            ApplyLocalPositionToVisuals(wheels.rightWheel);
            
            //rear
            wheels = axleInfos[1];
            if (wheels.steering) {
                wheels.leftWheel.steerAngle = -steering;
                wheels.rightWheel.steerAngle = -steering;
            }

            if (wheels.motor) {
                wheels.leftWheel.motorTorque = motor;
                wheels.rightWheel.motorTorque = motor;
            }

            if (wheels.brake) {
                if (_brake) {
                    wheels.leftWheel.brakeTorque = maxBrake;
                    wheels.rightWheel.brakeTorque = maxBrake;
                }
                else {
                    wheels.leftWheel.brakeTorque = 0;
                    wheels.rightWheel.brakeTorque = 0;
                }
            }

            //_Rpm = axleInfo.rightWheel.rpm; // округление и перевод в int
            ApplyLocalPositionToVisuals(wheels.leftWheel);
            ApplyLocalPositionToVisuals(wheels.rightWheel);
       // }
    }


    private void Update() {
        PlayerCarControl();
        ApplyDrift();
        // var wheelSidewaysFriction = axleInfos[0].leftWheel.sidewaysFriction;
        // wheelSidewaysFriction.stiffness = driftFactor; 

        // var leftWheelSidewaysFriction = axleInfos[0].leftWheel.sidewaysFriction;
        // leftWheelSidewaysFriction.stiffness = 0.5f;
        //
        // axleInfos[0].leftWheel.sidewaysFriction = leftWheelSidewaysFriction;


    }
    
    
    
    private void ApplyDrift() { 
        foreach (Wheels axleInfo in axleInfos) { 
            if (IsDrifting()) {
                Debug.Log("IsDrifting ");
                
                var wheelSidewaysFriction = axleInfo.leftWheel.sidewaysFriction;
                wheelSidewaysFriction.stiffness = driftFactor; 
                
                wheelSidewaysFriction = axleInfo.rightWheel.sidewaysFriction;
                wheelSidewaysFriction.stiffness = driftFactor;
            } 
            else { 
                var wheelSidewaysFriction = axleInfo.leftWheel.sidewaysFriction;
                wheelSidewaysFriction.stiffness = normalFactor; 
                
                wheelSidewaysFriction = axleInfo.rightWheel.sidewaysFriction;
                wheelSidewaysFriction.stiffness = normalFactor;
            } 
        } 
    }

    private bool IsDrifting() {
        bool isDrifting = Mathf.Abs(_horizontal) > 0.1f;
       // Debug.Log(Mathf.Abs(_horizontal));
            
        //Debug.Log("horizontal " + Mathf.Abs(_horizontal));
        // foreach (AxleInfo axleInfo in axleInfos) {
        //     axleInfo.leftWheel.GetGroundHit(out var wheelHit); 
        //     float sidewaysSlip = Mathf.Abs(wheelHit.sidewaysSlip); 
        //     if (sidewaysSlip > 0.2f) { 
        //         isDrifting = true; 
        //     } 
        // }

        return isDrifting;
    } 

    private void PlayerCarControl() {
        _vertical = Input.GetAxis("Vertical");      
        _horizontal = Input.GetAxis("Horizontal");  
        _brake = Input.GetButton("Jump");
        
        // _vertical = Input.acceleration.y;
        // _horizontal = Input.acceleration.x;
    }
}