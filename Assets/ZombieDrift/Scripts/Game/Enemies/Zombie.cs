using UnityEngine;

public class Zombie : MonoBehaviour, IDamagable {
    [SerializeField] private Animator animator;
    [SerializeField] private Collider triggerCollider;
    [SerializeField] private Ragdoll ragdoll;
    private ZombieAnimator _zombieAnimator;

    public void Awake() {
        _zombieAnimator = new ZombieAnimator();
        _zombieAnimator.Initialize(animator);

        ragdoll.Initialize();
        ragdoll.isEnabled = false;
    }

    public void Damage() {
        _zombieAnimator.isEnabled = false;
        ragdoll.isEnabled = true;
        triggerCollider.enabled = false;
    }
}