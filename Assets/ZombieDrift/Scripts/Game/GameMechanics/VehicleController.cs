using UnityEngine;

public class VehicleController {
    private readonly IInput _input;

    private Car _car;

    // Когда загружается уровень, машина едет не сразу, а после первого нажания вправо или влево  
    private bool _isOnesTouched;

    public VehicleController(IInput input) {
        _input = input;
    }

    public void Initialize(Car car) {
        _car = car;
        _isOnesTouched = false;
    }

    public void Start() {
        _input.HorizontalAxisChangedEvent += OnTurn;

        if (_isOnesTouched)
            RunVehicle(true);
    }

    public void Stop() {
        _isOnesTouched = false;
        _input.HorizontalAxisChangedEvent -= OnTurn;
        RunVehicle(false);
    }

    private void RunVehicle(bool isRun) {
        _car.motor.isRun = isRun;
    }

    private void OnTurn(float axisHorizontal) {
        _car.motor.steerTurn = axisHorizontal;

        if (_isOnesTouched) return;

        _isOnesTouched = true;
        RunVehicle(true);
    }
}