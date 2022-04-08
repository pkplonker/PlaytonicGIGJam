using System.Collections;
using System.Collections.Generic;
using Character.Player;
using SO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpecialItemPickup : MonoBehaviour
{
	[SerializeField] private CanvasGroup container;
	[SerializeField] private ItemBase notItem;
	[SerializeField] private TextMeshProUGUI quantityText;
	[SerializeField] private TextMeshProUGUI nameText;
	[SerializeField] private Image itemSprite;
	[SerializeField] private float aliveTime;
	[SerializeField] private float fadeSpeed;
	private Inventory inventory;

	private Coroutine fadeCor;

	private void Start()
	{
		nameText.text = "";

		quantityText.text = "";
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
		if (slot.item == notItem) return;
		UpdateUI(slot);
	}

	private void UpdateUI(Slot slot)
	{
		container.alpha = 1;
		quantityText.text = slot.amount.ToString();
		nameText.text = slot.item.itemName;
		itemSprite.sprite = slot.item.itemSprite;
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