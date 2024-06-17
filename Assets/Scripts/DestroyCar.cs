using System;
using UnityEngine;

[Serializable]
public class DestroyCar {
    [SerializeField] private Rigidbody rigidbodyCar;
    [SerializeField] private Transform[] wheels;
    [SerializeField] private Rigidbody[] rigidbodyWheels;
    [SerializeField] private Collider colliderCar;
    
    public float radius = 20f;
    public float force = 500f;
    
    //public void ShowDestroy(Collision collision) {
    public void ShowDestroy(Vector3 moveForceVector) {
        rigidbodyCar.isKinematic = true;
        colliderCar.enabled = false;
        for (int i = 0; i < rigidbodyWheels.Length; i++) {
            wheels[i].parent = null;
            rigidbodyWheels[i].isKinematic = false;
            rigidbodyWheels[i].AddForce(moveForceVector * force, ForceMode.Impulse);
        }
    }
    
    private void Explode() {
        Collider[] overlappedColliders = Physics.OverlapSphere(rigidbodyCar.transform.position, radius);
        Debug.Log("overlappedColliders.Length " + overlappedColliders.Length);
        int count = 0;
        for (int j = 0; j < overlappedColliders.Length; j++) {
            Rigidbody rigidbody = overlappedColliders[j].attachedRigidbody; 
            if (rigidbody) {
                rigidbody.AddExplosionForce(force, rigidbodyCar.transform.position, radius);
                count++;
            }
        }
        
        Debug.Log("AddExplosionForce count " + count);
    }
}
