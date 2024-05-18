using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class AxleInfo {
    public WheelCollider leftWheel; // коллайдеры
    public WheelCollider rightWheel; // колес
    public bool motor; // вращение колес
    public bool steering; // повороты колес
    public bool brake; // ручной тормоз
}

public class CarController : MonoBehaviour {
    [SerializeField] private List<AxleInfo> axleInfos;
    [SerializeField] private Rigidbody rigidBody;
    [SerializeField] private float maxMotorTorque;
    [SerializeField] private float maxSteeringAngle;
    [SerializeField] private float maxbrake;
    private float _vertical;
    private float _horizontal;
    private bool _brake;


    private void Awake() {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
    }


    // нахождение визуальных колес и манипулирование ими
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
                    axleInfo.leftWheel.brakeTorque = maxbrake;
                    axleInfo.rightWheel.brakeTorque = maxbrake;
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
    }

    private void PlayerCarControl() {
        _vertical = Input.GetAxis("Vertical");      
        _horizontal = Input.GetAxis("Horizontal");  
        _brake = Input.GetButton("Jump");
        
        // _vertical = Input.acceleration.y;
        // _horizontal = Input.acceleration.x;
    }
}