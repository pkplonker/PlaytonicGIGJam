using System;
using System.Collections;
using System.Collections.Generic;
using Character.Player;
using SO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
	public class CrystalsUI : MonoBehaviour
	{
		[SerializeField] private CanvasGroup container;
		[SerializeField] private ItemBase item;
		private TextMeshProUGUI text;
		private Inventory inventory;
		[SerializeField] private float aliveTime;
		[SerializeField] private float fadeSpeed;
		private Coroutine fadeCor;
		private void Start()
		{
			text = GetComponentInChildren<TextMeshProUGUI>();
			text.text = "";
			container.alpha = 0;
		}

		private void OnEnable()
		{
			inventory = PlayerController.instance.GetComponent<Inventory>();

			inventory.OnItemPickedUp += ItemPickUp;
		}

		private void OnDisable()
		{
			inventory.OnItemPickedUp -= ItemPickUp;
		}

		private void ItemPickUp(Slot slot)
		{
			if (slot.item != item) return;
			UpdateUI(slot.amount);
		}

		private void UpdateUI(int newAmount)
		{
			container.alpha = 1;
			text.text = newAmount.ToString();
			RestartCor();
		}

		private void RestartCor()
		{
			if (fadeCor != null)
			{
				StopCoroutine(fadeCor);
				fadeCor = null;
				container.alpha = 1;

			}

			fadeCor = StartCoroutine(Fade());
		}

		private IEnumerator Fade()
		{
			float elapsedTime = 0;
			while (elapsedTime < aliveTime)
			{
				elapsedTime += Time.deltaTime;
				yield return null;
			}

			elapsedTime = 0;
			while (elapsedTime < fadeSpeed)
			{
				elapsedTime += Time.deltaTime;
				container.alpha = Mathf.Lerp(1, 0, elapsedTime / fadeSpeed);
				yield return null;
			}

			fadeCor = null;
		}
	}
}