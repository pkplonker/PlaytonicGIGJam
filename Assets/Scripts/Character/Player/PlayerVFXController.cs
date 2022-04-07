using System;
using UnityEngine;

namespace Character.Player
{
	public class PlayerVFXController : MonoBehaviour
	{
		#region Variables
		[SerializeField] private Transform groundVFXTarget;

		[SerializeField] private GameObject jumpVFX;
		[SerializeField] private GameObject landVFX;
		[SerializeField] private GameObject walkVFX;
		[SerializeField] private GameObject attackVFX;
		[SerializeField] private GameObject rollVFX;
		[SerializeField] private GameObject minorRewardVFX;
		[SerializeField] private GameObject majorRewardVFX;
		private PlayerLocomotion playerLocomotion;
		#endregion

		private void Awake()
		{
			playerLocomotion = GetComponent<PlayerLocomotion>();
		}

		private void OnEnable()
		{
			playerLocomotion.OnLand += PlayLandVFX;
			playerLocomotion.OnJump += PlayJumpVFX;
			playerLocomotion.OnGroundedChange += GroundChange;

			
		}
		private void OnDisable()
		{
			playerLocomotion.OnLand -= PlayLandVFX;
			playerLocomotion.OnJump -= PlayJumpVFX;
			playerLocomotion.OnGroundedChange -= GroundChange;


		}

		private void GroundChange(bool b)
		{
			walkVFX.SetActive(b);
		}
		private void PlayJumpVFX()
		{
		GameObject obj = Instantiate(jumpVFX, groundVFXTarget.position, groundVFXTarget.rotation);
		Destroy(obj,2f);
		}

		private void PlayLandVFX()
		{
			GameObject obj = Instantiate(landVFX, groundVFXTarget.position, groundVFXTarget.rotation);
			Destroy(obj,2f);
		}


		public void Pickup(bool isMajor)
		{
			GameObject obj = Instantiate(isMajor? majorRewardVFX: minorRewardVFX , groundVFXTarget.position, groundVFXTarget.rotation);
			obj.transform.parent = gameObject.transform;
			Destroy(obj,2f);
		}
	}
}
