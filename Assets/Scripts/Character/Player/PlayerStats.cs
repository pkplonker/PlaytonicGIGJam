using UnityEngine;

namespace Character.Player
{
    public class PlayerStats : CharacterStats
    {
        private Animator animator;
        private static readonly int IsDead = Animator.StringToHash("isDead");

        protected override void Start()
        {
           base.Start();
           animator = GetComponentInChildren<Animator>();
        }
        protected override void Die()
        {
            base.Die();
            animator.Play("Death");
            animator.SetBool(IsDead,true);
        }
    }
}
