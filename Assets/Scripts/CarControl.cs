using System;
using UnityEngine;

public class CarControl : MonoBehaviour {
    [SerializeField] private View goodCar; 
    [SerializeField] private View destroyCar; 
    private IInputControllable inputControl = new InputPC();
    [SerializeField] private Drive drive;
    private const float accel = 1.0f;
    
    private void Awake() {
        Subscribe();
    }

    private void Update() {
        inputControl.Update();
    }

    private void OnCollisionEnter(Collision collision) {
        bool isWall = collision.gameObject.TryGetComponent(out Wall wall);
        if (isWall) {
            DestroyCar();
        }
    }

    private void Subscribe() {
        inputControl.InputControlEvent += OnInputControl;
    }

    private void OnInputControl(Vector2 axis) {
        axis.y = accel;
        drive.UpdateDrive(axis);
    }

    private void UnSubscribe() {
        inputControl.InputControlEvent -= OnInputControl;
    }

    private void OnDestroy() {
        UnSubscribe();
    }

    private void DestroyCar() {
        UnSubscribe();
        drive.StopCar();
        goodCar.Close();
        destroyCar.Open();
    }
    
}
