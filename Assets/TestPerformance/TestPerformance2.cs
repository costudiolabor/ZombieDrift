using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class TestPerformance2 : MonoBehaviour, ITestPerformance2
{
   [SerializeField] private int count = 10000;
   private Stopwatch stopwatch = new Stopwatch();
   //private int a;
   TestPerformance2 testGo2;
   [SerializeField] private Transform _transform;
   [SerializeField] private Camera mainCamera;

   [SerializeField] private BehaviourScript behaviourScript;
   [SerializeField] private Script script = new Script();
   

   private void Awake()
   {
      Debug.Log("Count " + count);
      int i = 0;
      stopwatch.Start();
      for (i = 0; i < count; i++) {
         if (gameObject.tag == "Hand") {
            //a = i;
         }
      }
      stopwatch.Stop();
      Debug.Log("tag " + stopwatch.ElapsedMilliseconds + " ms ");
      stopwatch.Reset();

      stopwatch.Start();
      for (i = 0; i < count; i++) {
         if (gameObject.CompareTag("Hand")) {
            //a = i;
         }
      }
      stopwatch.Stop();
      Debug.Log("CompareTag " + stopwatch.ElapsedMilliseconds + " ms ");
      stopwatch.Reset();

      stopwatch.Start();
      for (i = 0; i < count; i++) {
         if (gameObject.TryGetComponent(out TestPerformance2 testGo)) {
            //a = i;
         }
      }
      stopwatch.Stop();
      Debug.Log("TryGetComponent " + stopwatch.ElapsedMilliseconds + " ms ");
      stopwatch.Reset();

      
      stopwatch.Start();
      for (i = 0; i < count; i++) {
         if (gameObject.TryGetComponent(out ITestPerformance2 testGo)) {
            //a = i;
         }
      }
      stopwatch.Stop();
      Debug.Log("TryInterface " + stopwatch.ElapsedMilliseconds + " ms ");
      stopwatch.Reset();
     
      
      
      stopwatch.Start();
      for (i = 0; i < count; i++)
      {
         //testGo2 = gameObject.GetComponent<TestPerformance2>();
         if (transform.GetComponent<TestPerformance2>()) {
            // a = i;
         }
      }
      stopwatch.Stop();
      Debug.Log("transform.GetComponent " + stopwatch.ElapsedMilliseconds + " ms ");
      stopwatch.Reset();
      
      
     
      stopwatch.Start();
      for (i = 0; i < count; i++)
      {
         //testGo2 = gameObject.GetComponent<TestPerformance2>();
         if (gameObject.GetComponent<TestPerformance2>()) {
           // a = i;
         }
      }
      stopwatch.Stop();
      Debug.Log("gameObject.GetComponent " + stopwatch.ElapsedMilliseconds + " ms ");
      stopwatch.Reset();
      
      
      stopwatch.Start();
      for (i = 0; i < count; i++)
      {
         //testGo2 = gameObject.GetComponent<TestPerformance2>();
         if (gameObject.GetComponent<ITestPerformance2>() != null) {
            // a = i;
         }
      }
      stopwatch.Stop();
      Debug.Log("GetInterface " + stopwatch.ElapsedMilliseconds + " ms ");
      stopwatch.Reset();
      
      
      stopwatch.Start();
      for (i = 0; i < count; i++)
      {
         if (transform) {
            
         }
      }
      stopwatch.Stop();
      Debug.Log("transform " + stopwatch.ElapsedMilliseconds + " ms ");
      stopwatch.Reset();
      
      
      stopwatch.Start();
      for (i = 0; i < count; i++)
      {
         if (_transform) {
            
         }
      }
      stopwatch.Stop();
      Debug.Log("_transform " + stopwatch.ElapsedMilliseconds + " ms ");
      stopwatch.Reset();
      
      
      stopwatch.Start();
      for (i = 0; i < count; i++)
      {
         if (Camera.main) {
            
         }
      }
      stopwatch.Stop();
      Debug.Log("Camera.main " + stopwatch.ElapsedMilliseconds + " ms ");
      stopwatch.Reset();
      
      
      stopwatch.Start();
      for (i = 0; i < count; i++)
      {
         if (mainCamera) {
            
         }
      }
      stopwatch.Stop();
      Debug.Log("mainCamera " + stopwatch.ElapsedMilliseconds + " ms ");
      stopwatch.Reset();

      
      
      stopwatch.Start();
      for (i = 0; i < count; i++)
      {
         behaviourScript.SetPos();
      }
      stopwatch.Stop();
      Debug.Log("behaviourScript.SetPos " + stopwatch.ElapsedMilliseconds + " ms ");
      stopwatch.Reset();

      
      stopwatch.Start();
      for (i = 0; i < count; i++)
      {
         script.SetPos();
      }
      stopwatch.Stop();
      Debug.Log("script.SetPos " + stopwatch.ElapsedMilliseconds + " ms ");
      stopwatch.Reset();
      
      
      stopwatch.Start();
      for (i = 0; i < count; i++)
      {
         script.SetPosLambda();
      }
      stopwatch.Stop();
      Debug.Log("script.SetPosLambda " + stopwatch.ElapsedMilliseconds + " ms ");
      stopwatch.Reset();


   }
   
   
   
   
   
   

   public void SetValue() {
      
   }
}



public interface ITestPerformance2 {
   public void SetValue();
}
