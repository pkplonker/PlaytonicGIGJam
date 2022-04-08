using System;
using System.Collections.Generic;
using SO;
using UnityEngine;

namespace Character.Player
{
	public class Inventory : MonoBehaviour
	{
		private List<Slot> slots;
		public event Action<Slot> OnItemPickedUp;

		private void Awake()
		{
			if (slots == null) slots = new List<Slot>();
			
		}

		public void AddItem(ItemBase newItem, int amount)
		{
			foreach (var slot in slots)
			{
				if (slot.item == newItem)
				{
					slot.amount += amount;
					OnItemPickedUp?.Invoke(slot);
					return;
				}
			}
			Slot newSlot = new Slot(newItem,amount);
			slots.Add(newSlot);
			OnItemPickedUp?.Invoke(newSlot);

		}
	}
}

[Serializable]
public class Slot
{
	public int amount;
	public ItemBase item;

	public Slot(ItemBase item, int amount)
	{
		this.item = item;
		this.amount = amount;
	}
}