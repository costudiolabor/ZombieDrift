using Zenject;

public abstract class State  {
    protected readonly StateSwitcher switcher;

    public State(StateSwitcher stateSwitcher) {
        switcher = stateSwitcher;
    }

    public virtual void Enter() {
    }

    public virtual void Exit() {
    }
    
    public virtual void FixedTick() {
    }

    public virtual void Tick() {
        
    }
}