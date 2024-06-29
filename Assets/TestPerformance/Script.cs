using System;
using UnityEngine;

[Serializable]
public class Script {
   public Transform thisTransform;

   public bool isTrue = false;
   public void SetPos() {
      thisTransform.position = new Vector3(0, 0, 0);
      isTrue = true;
   }
   
   public void SetPosLambda() => thisTransform.position = new Vector3(0, 0, 0);
   
}
