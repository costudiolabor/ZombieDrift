using UnityEngine;

public class ZombieAnimator  {
   private Animator _animator;

   public bool isEnabled {
      get => _animator.enabled;
      set => _animator.enabled = value;
   }

   public void Initialize(Animator animator) {
      _animator = animator;
   }
}