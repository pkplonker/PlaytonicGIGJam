using System;
using Character;
using Character.Player;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
	public class StatBarUI : MonoBehaviour
	{
		[SerializeField] protected CharacterStats stats;
		
		protected Slider slider;
		
		protected virtual void Awake()
		{
			slider = GetComponent<Slider>();
			if (stats == null)
			{
				if(stats==null) stats = PlayerController.instance.GetComponent<CharacterStats>();
			}
		}

		protected virtual void Start()
		{
			
		}

		protected virtual void SetCurrentStat(float value)
		{
			slider.value = value;
		}
		protected void SetEmpty()
		{
			slider.value = 0;
		}

		protected virtual void Death()
		{
			SetEmpty();
		}

	
	}
}