using UnityEngine;

public class CubeArray : MonoBehaviour {

    [SerializeField] private Transform[] cubes;
    private Vector3 speed;
    private void Start() {
        speed = new Vector3(1.5f, 1.2f, 1.1f);
    }

    void Update() {
        foreach (var cube in cubes) {
            cube.Rotate(speed);
        }
    }
}
