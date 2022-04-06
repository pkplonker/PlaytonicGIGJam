using System;
using UnityEngine;

namespace Character
{
    public class AnimationHandler : MonoBehaviour
    {
        private Animator animator;
        private static readonly int Vertical = Animator.StringToHash("Vertical");
        private static readonly int Horizontal = Animator.StringToHash("Horizontal");

        protected virtual void Awake()
        {
            animator = GetComponent<Animator>();
        }

        public virtual void UpdateLocomotion(float moveAmount)
        {
           
            animator.SetFloat(Vertical, Mathf.Abs(moveAmount));

        }
    }
}
