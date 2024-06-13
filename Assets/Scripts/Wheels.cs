using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wheels : MonoBehaviour {
   [SerializeField] private Transform[] wheelsFront;
   [SerializeField] private Transform[] wheelsRear;

   [SerializeField] private Car car;

   private void Update() {
      Vector2 axis = car.GetAxis();

      foreach (var wheel in wheelsRear) {
         //wheel.RotateAround(Vector3.left, axis.y);
         wheel.Rotate(Vector3.left, -axis.y);
      }
      
      foreach (var wheel in wheelsFront) {
         //wheel.RotateAround(Vector3.left, axis.y);
         wheel.Rotate(Vector3.left, -axis.y);
         wheel.Rotate(Vector3.up, -axis.x);
      }
   }
}
