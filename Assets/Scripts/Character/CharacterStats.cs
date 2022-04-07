using System;
using UnityEngine;

namespace Character
{
    public class CharacterStats : MonoBehaviour
    {
        #region Variables

        [SerializeField] private string characterName;
        [SerializeField] private float maxHealth;
        [SerializeField] private float maxStamina;
        private float currentHealth;
        private float currentStamina;
        public bool canMove { get; private set; } = true;
        public event Action OnDeath;
        public event Action<float> OnHealthChanged;
        public event Action<float> OnStaminaChanged;
        #endregion

        protected void Awake()
        {
            currentHealth = maxHealth;
            currentStamina = maxStamina;
        }

        protected virtual void Die()
        {
            canMove = false;
            OnDeath?.Invoke();
        }

        public virtual void Heal(float amount)
        {
            if (amount <= 0) return;
            currentHealth += amount;
            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }
            OnHealthChanged?.Invoke(currentHealth);
        }
        public virtual void TakeDamage(float amount)
        {
            if (amount < 0) return;
            currentHealth -= amount;
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                Die();
            }
            OnHealthChanged?.Invoke(currentHealth);
        }
        public virtual void HealStamina(float amount)
        {
            if (amount <= 0) return;
            currentStamina += amount;
            if (currentStamina > maxStamina)
            {
                currentStamina = maxStamina;
            }
            OnStaminaChanged?.Invoke(currentStamina);
        }
        public virtual void TakeStaminaDamage(float amount)
        {
            if (amount < 0) return;
            currentStamina -= amount;
            if (currentStamina <= 0)
            {
                currentStamina = 0;
            }
            OnStaminaChanged?.Invoke(currentStamina);
        }

        public bool  HasEnoughStamina(float amount)=>currentStamina - amount >= 0;
        
    }
}
