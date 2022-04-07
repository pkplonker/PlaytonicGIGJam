using System;
using UnityEngine;

namespace Character.Player
{
	public class PlayerLocomotion : MonoBehaviour
	{
		#region Variables

		private CharacterController controller;

		[SerializeField] private float gravity = -9.81f;

		[SerializeField] private float moveSpeed;
		[SerializeField] private float rotationSpeed;
		[SerializeField] private float jumpAmount = 20f;
		[SerializeField] private Transform groundCheckLocation;
		private Vector3 moveDirection;
		private InputHandler inputHandler;
		private AnimationHandler animationHandler;
		private Transform playerCamera;
		private float yDirection;
		private bool isGrounded;

		#endregion

		private void Awake()
		{
			inputHandler = GetComponent<InputHandler>();
			controller = GetComponent<CharacterController>();
			animationHandler = GetComponentInChildren<AnimationHandler>();
			playerCamera = PlayerCameraController.instance.GetComponentInChildren<Camera>().transform;
		}

		private void Update()
		{
			GroundCheck();
			UpdateMovement();
			UpdateRotation();
			HandleVerticalMovement();
			FinalMovement();
		}

		private void GroundCheck()
		{
			Debug.DrawLine(groundCheckLocation.position, groundCheckLocation.position-new Vector3(0,0.3f,0), Color.red, 0.1f);
			isGrounded = Physics.Raycast(groundCheckLocation.position, Vector3.down, 0.3f);
		}

		private void FinalMovement()
		{
			
			moveDirection *= Time.deltaTime;
			animationHandler.UpdateLocomotion(Mathf.Clamp01(Mathf.Abs(inputHandler.horizontalInput) +
			                                                Mathf.Abs(inputHandler.verticalInput)));
			controller.Move(moveDirection);
		}

		private void HandleVerticalMovement()
		{
			
			if(isGrounded) yDirection = 0;
			else  yDirection += gravity*Time.deltaTime;
			if (isGrounded && inputHandler.inputA)
			{
				Debug.Log("jumping");
				yDirection += jumpAmount;
			}
		}

	


		private void UpdateRotation()
		{
			Vector3 targetDirection = Vector3.zero;
			targetDirection = playerCamera.forward * inputHandler.verticalInput;
			targetDirection += playerCamera.right * inputHandler.horizontalInput;
			targetDirection.Normalize();
			targetDirection.y = 0;
			if (targetDirection == Vector3.zero) targetDirection = transform.forward;
			Quaternion tr = Quaternion.LookRotation(targetDirection);
			Quaternion targetRotation =
				Quaternion.Slerp(transform.rotation, tr, rotationSpeed * Time.deltaTime);
			transform.rotation = targetRotation;
		}

		private void UpdateMovement()
		{
			moveDirection = playerCamera.forward * inputHandler.verticalInput;
			moveDirection += playerCamera.right * inputHandler.horizontalInput;
			moveDirection.Normalize();
			moveDirection *= moveSpeed;
		}
	}
}