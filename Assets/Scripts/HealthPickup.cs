using System.Collections;
using System.Collections.Generic;
using Character;
using Character.Player;
using UnityEngine;

public class HealthPickup : PickUp
{
    
    protected override void Interact(CharacterStats stats)
    {
        base.Interact(stats);
        stats.Heal(amount);
        

    }
}
