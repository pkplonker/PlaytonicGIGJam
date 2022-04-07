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
		[SerializeField] private ParticleSystem attackVFX;
		[SerializeField] private ParticleSystem rollVFX;
		[SerializeField] private ParticleSystem minorRewardVFX;
		[SerializeField] private ParticleSystem majorRewardVFX;
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

		
	}
}
