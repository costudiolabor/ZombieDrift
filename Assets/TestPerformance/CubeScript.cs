using UnityEngine;

public class CubeScript : MonoBehaviour {
    [SerializeField] private Vector3 speed;
    private Transform thisTransform;
    
    private void Start() {
        thisTransform = transform;
        speed = new Vector3(1.5f, 1.2f, 1.1f);
    }

    void Update() {
        thisTransform.Rotate(speed);
    }
}
