using System;
using System.Collections;
using System.Collections.Generic;
using Character;
using UnityEngine;

public class DamageBlock : MonoBehaviour
{
	[SerializeField] private float damageAmount = 10f;
	private void OnTriggerEnter(Collider other)
	{
		CharacterStats stats = other.GetComponent<CharacterStats>();
		if (other == null) return;
		
		stats.TakeDamage(damageAmount);
		
	}
}
