public class HowToPlayPresenter {
    private const int HIDE_DELAY = 1500;
    private HowToPlayView _view;

    public bool enabled {
        set => _view.isVisible = value;
    }

    public void Initialize(HowToPlayView view) {
        _view = view;
    }

    public void HideWithDelay() {
        _view.HideWithDelay(HIDE_DELAY);
    }
}