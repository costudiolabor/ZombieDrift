using System;
using UnityEngine;

[Serializable]
public class DestroyWheels {
    //[SerializeField] private Rigidbody rigidbodyCar;
    [SerializeField] private Transform[] wheels;
    //[SerializeField] private Rigidbody[] rigidbodyWheels;
    
    public float force = 500f;
    
    public void Init() {
        
    }
    
    public void ShowDestroy(Vector3 moveForceVector) {
        //rigidbodyCar.isKinematic = true;
        // for (int i = 0; i < rigidbodyWheels.Length; i++) {
        //     wheels[i].parent = null;
        //     rigidbodyWheels[i].isKinematic = false;
        //     rigidbodyWheels[i].AddForce(moveForceVector * force, ForceMode.Impulse);
        // }
        //
        for (int i = 0; i < wheels.Length; i++) {
            wheels[i].parent = null;
            Rigidbody rigidbodyWheel = wheels[i].gameObject.AddComponent<Rigidbody>();
            rigidbodyWheel.AddForce(moveForceVector * force, ForceMode.Impulse);
        }
    }
}
