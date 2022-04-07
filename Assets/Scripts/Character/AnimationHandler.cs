using System;
using UnityEngine;

namespace Character
{
    public class AnimationHandler : MonoBehaviour
    {
        private Animator animator;
        private static readonly int Vertical = Animator.StringToHash("Vertical");
        private static readonly int Horizontal = Animator.StringToHash("Horizontal");
        private static readonly int IsInteracting = Animator.StringToHash("isInteracting");

        protected virtual void Awake()
        {
            animator = GetComponent<Animator>();
        }

        public virtual void UpdateLocomotion(float moveAmount)
        {
           
            animator.SetFloat(Vertical, Mathf.Abs(moveAmount));

        }
        
        public void PlayTargetAnimation(string targetAnim)
        {
            if (targetAnim == "") return;
            animator.CrossFade(targetAnim, 0.2f);
        }
    }
}
