using System.Collections;
using System.Collections.Generic;
using Character;
using UnityEngine;

public class StaminaPickup : Interactable
{
    [SerializeField] private float amount;

    protected override void Interact(CharacterStats stats)
    {
	    base.Interact(stats);
	    stats.HealStamina(amount);
	    Destroy(gameObject,0.3f);


    }
}
