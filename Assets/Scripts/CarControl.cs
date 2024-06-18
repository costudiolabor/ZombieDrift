using UnityEngine;
using UnityEngine.Serialization;

public class CarControl : MonoBehaviour {
    [SerializeField] private Rigidbody rigidbodyCar;
    [SerializeField] private Collider colliderCar;
    [SerializeField] private Drive drive;
    [SerializeField] private Wheels wheels;
    [SerializeField] private DestroyWheels destroyWheels;
    [SerializeField] private VFXCar vfxCar;
    private IInputControllable _inputControl = new InputPC();

    private void Awake() {
        wheels.Initialize();
        Subscribe();
    }
    private void FixedUpdate() { _inputControl .Update(); }
    private void OnCollisionEnter(Collision collision) {
        bool isWall = collision.gameObject.TryGetComponent(out Wall wall);
        if (isWall) { DestroyCar(); }
    }
    private void Subscribe() { _inputControl.InputControlEvent += OnInputControl; }

    private void OnInputControl(Vector2 axis) {
        drive.UpdateDrive(axis);
        wheels.UpdateWheels(axis);
        vfxCar.UpdateVFXCar(axis);
    }

    private void UnSubscribe() { _inputControl.InputControlEvent -= OnInputControl; }
    private void OnDestroy() { UnSubscribe(); }
    private void DestroyCar() {
        UnSubscribe();
        drive.StopCar();
        vfxCar.StopParticles();
        colliderCar.enabled = false;
        rigidbodyCar.isKinematic = true;
        destroyWheels.ShowDestroy(drive.GetForceVector());
    }
}
