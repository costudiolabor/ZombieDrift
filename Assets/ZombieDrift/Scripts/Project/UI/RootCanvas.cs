using UnityEngine;

public class RootCanvas : MonoBehaviour {
    [SerializeField] private FadeView _screen;

    public FadeView loading => _screen;


    private void Awake() =>
        HideLoadingScreen();

    public void HideLoadingScreen() =>
        _screen.gameObject.SetActive(false);
}