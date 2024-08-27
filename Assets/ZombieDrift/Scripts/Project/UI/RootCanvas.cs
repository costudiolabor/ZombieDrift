using UnityEngine;
public class RootCanvas : MonoBehaviour {
    [SerializeField] private FadeLoadingView loadingScreen;
    [SerializeField] private GameObject sceneFade;

    public FadeLoadingView loading => loadingScreen;
    public GameObject fade => sceneFade;

    private void Awake() {
        HideLoadingScreen();
    }

    public void HideLoadingScreen() {
        loadingScreen.gameObject.SetActive(false);
    }
}
