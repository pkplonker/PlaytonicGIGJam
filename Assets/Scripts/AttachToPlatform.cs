using System;
using System.Collections;
using System.Collections.Generic;
using Character;
using UnityEngine;

public class AttachToPlatform : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		if (!other.CompareTag("Player")) return;
		CharacterStats stats = other.GetComponent<CharacterStats>();
		if (stats == null) return;
		stats.transform.parent = transform;
	}

	private void OnTriggerExit(Collider other)
	{
		if (!other.CompareTag("Player")) return;
		CharacterStats stats = other.GetComponent<CharacterStats>();
		if (stats == null) return;
		stats.transform.parent = null;
	}
}
