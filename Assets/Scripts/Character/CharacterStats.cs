using System;
using UnityEngine;

namespace Character
{
	public class CharacterStats : MonoBehaviour
	{
		#region Variables

		[SerializeField] private string characterName;
		[SerializeField] public float maxHealth=100f;
		[SerializeField] public float maxStamina=100f;
		public bool isDead { get; protected set; }
		public float currentHealth { get; protected set; }
		public float currentStamina{ get; protected set; }
		public bool canMove { get; protected set; } = true;
		public event Action OnDeath;
		public event Action<float> OnHealthChanged;
		public event Action<float> OnStaminaChanged;
		public event Action OnDamage;

		#endregion

		protected void Awake()
		{
			currentHealth = maxHealth;
			currentStamina = maxStamina;
			OnHealthChanged?.Invoke(currentHealth);

		}

		protected virtual void Start()
		{
			OnHealthChanged?.Invoke(currentHealth);
			OnStaminaChanged?.Invoke(currentStamina);
		}

		protected virtual void Die()
		{
			canMove = false;
			OnDeath?.Invoke();
			isDead = true;
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
			OnDamage?.Invoke();
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

		public bool HasEnoughStamina(float amount) => currentStamina - amount >= 0;

		public virtual void Revive()
		{
			currentHealth = maxHealth;
			currentStamina = maxStamina;
			isDead = false;
			canMove = true;
           
			OnStaminaChanged?.Invoke(currentStamina);
			OnHealthChanged?.Invoke(currentHealth);
		}
	}
}