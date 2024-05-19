using UnityEngine;

public class CarControl : MonoBehaviour {
    private IInputControllable inputControl = new InputControl();
    [SerializeField] private Drive drive;
    
    private void Start() {
        
    }

    private void Update() {
        Vector2 axis = inputControl.GetInputControl();
        drive.UpdateDrive(axis);
    }
}
