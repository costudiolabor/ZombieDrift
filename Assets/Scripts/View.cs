using UnityEngine;

public class View : MonoBehaviour {
    public void Open() {
        gameObject.SetActive(true);
    }

    public void Close() {
        gameObject.SetActive(false);
    }
}
