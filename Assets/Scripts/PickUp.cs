using System.Collections;
using System.Collections.Generic;
using Character;
using Character.Player;
using UnityEngine;

public class PickUp : Interactable
{
    [SerializeField] protected float amount;
    [SerializeField] protected bool isMajor=false;
    
    protected override void Interact(CharacterStats stats)
    {
        base.Interact(stats);
        stats.GetComponent<PlayerVFXController>().Pickup(isMajor);
        Destroy(gameObject, .2f);

    }
    
    
    

}
