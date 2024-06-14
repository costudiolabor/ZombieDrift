using System;
using UnityEngine;

[Serializable]
public class Script {
   public Transform thisTransform;

   public void SetPos() {
      thisTransform.position = new Vector3(0, 0, 0);
   }
   
   public void SetPosLambda() => thisTransform.position = new Vector3(0, 0, 0);
   
}
