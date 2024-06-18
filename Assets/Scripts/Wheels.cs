using System;
using UnityEngine;

[Serializable]
public class Wheels {
    [SerializeField] private Transform[] wheelsFront;
    [SerializeField] private Transform[] wheelsRear;
    [SerializeField] private float speedFrontWheel;
    [SerializeField] private float angleFrontWheel;
    [SerializeField] private float speedRearWheel;

    private Transform[] _visualWheels;
    private float _angle;

    public void Initialize() {
        _visualWheels = new Transform[wheelsFront.Length];
        for (int i = 0; i < wheelsFront.Length; i++) { _visualWheels[i] = wheelsFront[i].GetChild(0); }
    }

    public void UpdateWheels(Vector2 axis) {
        _angle = axis.x * angleFrontWheel;
        for (int i = 0; i < wheelsFront.Length; i++) {
            wheelsFront[i].localEulerAngles = new Vector3(0, _angle, 0);
            _visualWheels[i].Rotate(Vector3.left, - axis.y * speedFrontWheel);
        }
        foreach (var wheel in wheelsRear) { wheel.Rotate(Vector3.left, - axis.y * speedRearWheel); }
      
        // if (_axis.y != 0) vfxCar.PlayParticles();
        // else vfxCar.StopParticles();
    }
   
    
   // private void OnCollisionEnter(Collision collision) {
        //destroyCar.ShowDestroy(car.GetForceVector());
        //Debug.Log(collision.transform.name);
   // }
}


// public class Wheels : MonoBehaviour {
//    [SerializeField] private Car car;
//    [SerializeField] private Transform[] wheelsFront;
//    [SerializeField] private Transform[] wheelsRear;
//
//    [SerializeField] private float speedFrontWheel;
//    [SerializeField] private float angleFrontWheel;
//    [SerializeField] private float speedRearWheel;
//    [SerializeField] private VFXCar vfxCar = new VFXCar();
//    [SerializeField] private DestroyCar destroyCar = new DestroyCar();
//
//    private Transform[] _visualWheels;
//    private Vector2 _axis;
//    private float _angle;
//
//    private void Awake() { Initialize(); }
//
//    private void Initialize() {
//       _visualWheels = new Transform[wheelsFront.Length];
//       for (int i = 0; i < wheelsFront.Length; i++) { _visualWheels[i] = wheelsFront[i].GetChild(0); }
//    }
//
//    private void Update() {
//       _axis = car.GetAxis();
//       _angle = _axis.x * angleFrontWheel;
//   
//       for (int i = 0; i < wheelsFront.Length; i++) {
//          wheelsFront[i].localEulerAngles = new Vector3(0, _angle, 0);
//          _visualWheels[i].Rotate(Vector3.left, -_axis.y * speedFrontWheel);
//       }
//       
//       foreach (var wheel in wheelsRear) { wheel.Rotate(Vector3.left, -_axis.y * speedRearWheel); }
//       
//       if (_axis.y != 0) vfxCar.PlayParticles();
//       else vfxCar.StopParticles();
//    }
//    
//    private void OnCollisionEnter(Collision collision) {
//       destroyCar.ShowDestroy(car.GetForceVector());
//       Debug.Log(collision.transform.name);
//    }
// }
