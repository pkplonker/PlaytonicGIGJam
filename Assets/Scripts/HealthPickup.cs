using System.Collections;
using System.Collections.Generic;
using Character;
using Character.Player;
using UnityEngine;

public class HealthPickup : Interactable
{
    [SerializeField] private float amount;
 

    protected override void Interact(CharacterStats stats)
    {
        base.Interact(stats);
        stats.Heal(amount);
        stats.GetComponent<PlayerVFXController>().Pickup(false);
        Destroy(gameObject,0.2f);

    }
}
