using UnityEngine;

public class View : MonoBehaviour
{
    public bool isVisible {
        set => gameObject.SetActive(value);
    }
}
