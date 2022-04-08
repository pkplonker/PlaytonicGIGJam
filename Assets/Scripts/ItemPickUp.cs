using System.Collections;
using System.Collections.Generic;
using Character;
using Character.Player;
using SO;
using UnityEngine;

public class ItemPickUp : PickUp
{
    [SerializeField] private GenericItem item;

    protected override void Interact(CharacterStats stats)
    {
        base.Interact(stats);
        stats.GetComponent<PlayerVFXController>().Pickup(isMajor);
      Inventory inventory =  stats.GetComponent<Inventory>();
      if(stats!=null) inventory.AddItem(item,(int)amount);
        Destroy(gameObject, .2f);
    }
}
