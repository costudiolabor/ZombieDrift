using UnityEngine;

public class BehaviourScript : MonoBehaviour
{
    public Transform thisTransform;

    public void SetPos() {
        thisTransform.position = new Vector3(0, 0, 0);
    }
    
    
}
