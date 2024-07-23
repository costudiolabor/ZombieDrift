using Cysharp.Threading.Tasks;

public class HowToPlayView : View {
    public async void HideWithDelay(int delayMilliseconds) {
        await UniTask.Delay(delayMilliseconds);
        gameObject.SetActive(false);
    }
}