using System.Collections;
using System.Collections.Generic;
using Character;
using Character.Player;
using UnityEngine;

public class StaminaPickup : Interactable
{
	[SerializeField] private float amount;

	protected override void Interact(CharacterStats stats)
	{
		base.Interact(stats);
		stats.HealStamina(amount);
		stats.GetComponent<PlayerVFXController>().Pickup(false);

		Destroy(gameObject, .2f);
	}
}