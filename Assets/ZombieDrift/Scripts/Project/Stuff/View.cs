using UnityEngine;

public class View : MonoBehaviour
{
    public bool isActive {
        set => gameObject.SetActive(value);
    }
}
