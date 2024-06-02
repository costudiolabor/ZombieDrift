using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Serialization;

[System.Serializable]
public class AxleInfo {
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;
    public bool motor; 
    public bool steering; 
    public bool brake; 
}

public class CarController : MonoBehaviour {
    [SerializeField] private List<AxleInfo> axleInfos;
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
        foreach (AxleInfo axleInfo in axleInfos) {
            if (axleInfo.steering) {
                axleInfo.leftWheel.steerAngle = steering;
                axleInfo.rightWheel.steerAngle = steering;
            }

            if (axleInfo.motor) {
                axleInfo.leftWheel.motorTorque = motor;
                axleInfo.rightWheel.motorTorque = motor;
            }

            if (axleInfo.brake) {
                if (_brake) {
                    axleInfo.leftWheel.brakeTorque = maxBrake;
                    axleInfo.rightWheel.brakeTorque = maxBrake;
                }
                else {
                    axleInfo.leftWheel.brakeTorque = 0;
                    axleInfo.rightWheel.brakeTorque = 0;
                }
            }

            //_Rpm = axleInfo.rightWheel.rpm; // округление и перевод в int
            ApplyLocalPositionToVisuals(axleInfo.leftWheel);
            ApplyLocalPositionToVisuals(axleInfo.rightWheel);
        }
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
        foreach (AxleInfo axleInfo in axleInfos) { 
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