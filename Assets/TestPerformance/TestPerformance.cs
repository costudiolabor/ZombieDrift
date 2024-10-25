using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class TestPerformance : MonoBehaviour {
    [SerializeField] private Transform _transform;
    private Stopwatch stopwatch = new Stopwatch();
    private const int Count = 1000000;
    private int count;
    private Transform temp;
    
    void Start() {
        count = Count;
        stopwatch.Start();
        for (int i = 0; i < count; i++) {
            temp = _transform;
        }
        stopwatch.Stop();
        Debug.Log("_transform " + stopwatch.ElapsedMilliseconds + " ms ");
        stopwatch.Reset();
        
        stopwatch.Start();
        for (int i = 0; i < count; i++) {
            temp = transform;
        }
        stopwatch.Stop();
        Debug.Log("transform " + stopwatch.ElapsedMilliseconds + " ms ");
        stopwatch.Reset();
    }
    
    
    
}
