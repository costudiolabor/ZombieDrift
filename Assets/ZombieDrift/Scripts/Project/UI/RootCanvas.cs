using UnityEngine;
public class RootCanvas : MonoBehaviour {
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private GameObject sceneFade;

    public GameObject loading => loadingScreen;
    public GameObject fade => sceneFade;

    private void Awake() {
        HideLoadingScreen();
    }

    public void ShowLoadingScreen() {
        loadingScreen.SetActive(true);
        sceneFade.SetActive(true);
    }
    public void HideLoadingScreen() {
        loadingScreen.SetActive(false);
        sceneFade.SetActive(false);
    }
}
