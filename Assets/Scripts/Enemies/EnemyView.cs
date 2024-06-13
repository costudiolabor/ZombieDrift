using UnityEngine;

public class EnemyView : MonoBehaviour, IActiveObstacle {
    [SerializeField] private Animator animator;
    [SerializeField] private Collider triggerCollider;
    [SerializeField] private Ragdoll ragdoll;
    private AnimationsHandler _animationsHandler;

    public void Awake() {
        _animationsHandler = new AnimationsHandler();
        _animationsHandler.Initialize(animator);
        
        ragdoll.Initialize();
        ragdoll.isEnabled = false;
    }

    public void Hit() {
        _animationsHandler.isAnimatorEnabled = false;
        ragdoll.isEnabled = true;
        triggerCollider.enabled = false;
    }
}
