using System;
using UnityEngine;

namespace Character
{
    public class AnimationHandler : MonoBehaviour
    {
        public Animator animator { get; private set; }
        private static readonly int Vertical = Animator.StringToHash("Vertical");
        

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
        public string GetCurrentAnimation()
        {
            Debug.Log(animator.GetCurrentAnimatorClipInfo(0)[0].clip.name);
            return animator.GetCurrentAnimatorClipInfo(0)[0].clip.name;
        }
    }
}
