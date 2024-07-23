public class VehicleController {
    private readonly IInput _input;
    private Car _car;

    public VehicleController(IInput input) =>
        _input = input;

    public void SetCar(Car car) =>
        _car = car;

    public void Start() {
        _input.HorizontalAxisChangedEvent += OnTurn;
        _car.isRunning = true;
    }

    public void Stop() {
        _input.HorizontalAxisChangedEvent -= OnTurn;
        _car.isRunning = false;
    }

    private void OnTurn(float axisHorizontal) =>
        _car.turnHorizontalAxis = axisHorizontal;
}