using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class TestPerformance2 : MonoBehaviour
{
   [SerializeField] private int count = 10000;
   private Stopwatch stopwatch = new Stopwatch();
   private int a;
   TestPerformance2 testGo2;
   [SerializeField] private Transform _transform;
   [SerializeField] private Camera mainCamera;

   private void Awake()
   {
      int i = 0;
      stopwatch.Start();
      for (i = 0; i < count; i++) {
         if (gameObject.tag == "Hand") {
            a = i;
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
      for (i = 0; i < count; i++)
      {
         //testGo2 = gameObject.GetComponent<TestPerformance2>();
         if (gameObject.GetComponent<TestPerformance2>()) {
           // a = i;
         }
      }
      stopwatch.Stop();
      Debug.Log("GetComponent " + stopwatch.ElapsedMilliseconds + " ms ");
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

      
   }
   
}
