using UnityEngine;

public class AnimationsHandler  {
   private Animator _animator;

   public bool isAnimatorEnabled {
      get => _animator.enabled;
      set => _animator.enabled = value;
   }

   public void Initialize(Animator animator) {
      _animator = animator;
     
   }
}