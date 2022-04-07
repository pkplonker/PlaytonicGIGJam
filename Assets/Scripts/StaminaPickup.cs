using System.Collections;
using System.Collections.Generic;
using Character;
using Character.Player;
using UnityEngine;

public class StaminaPickup : PickUp
{

	protected override void Interact(CharacterStats stats)
	{
		base.Interact(stats);
		stats.HealStamina(amount);
		

		
	}
}