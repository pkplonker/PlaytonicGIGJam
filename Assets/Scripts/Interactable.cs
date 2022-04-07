using System;
using System.Collections;
using System.Collections.Generic;
using Character;
using Character.Player;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    private bool interacted = false;
    protected void OnTriggerEnter(Collider other)
    {
        PlayerStats stats = other.GetComponent<PlayerStats>();
        if (other == null) return;
        Interact(stats);

    }

    protected virtual void Interact(CharacterStats stats)
    {
        interacted = true;

    }
}
