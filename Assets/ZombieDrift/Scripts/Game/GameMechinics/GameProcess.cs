using System.Collections.Generic;

public class Gameplay {
    private readonly IInput _input;
    private GameplayData _data;
    private List<Zombie> _zombies;
    private Car _car;
    private Map _map;

    public Gameplay(IInput input) {
        _input = input;
    }
    
    public void Initialize(GameplayData data) {
        _data = data;
        _zombies = new List<IDamageable>(_data.zombies);
        _car = data.car;
        
        _car.Initialize();
    }

    public void Clear() {
        
    }

    public void Start() {
        SubscribeInputEvents();
        _car.motor.isRun = true;
    }

    private void SubscribeInputEvents() {
        _input.HorizontalAxisChangedEvent += OnInput;
    }

    private void OnInput(float horizontalAxis) {
        _car.motor.steerTurn = horizontalAxis;
    }

    public void Stop() {
        
    }
}
