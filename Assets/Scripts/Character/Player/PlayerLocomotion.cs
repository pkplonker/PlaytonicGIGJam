using System;
using System.Collections;
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
		[SerializeField] private float jumpHeight = 3f;
		[SerializeField] private Transform groundCheckLocation;
		private InputHandler inputHandler;
		private AnimationHandler animationHandler;
		private Transform playerCamera;
		private Vector3 moveDirection;
		private int jumpsRemaining = 0;
		[SerializeField] private int jumpsAllowed;
		private bool isJumping;
		private bool isGrounded;

		#endregion

		private void Awake()
		{
			inputHandler = GetComponent<InputHandler>();
			controller = GetComponent<CharacterController>();
			animationHandler = GetComponentInChildren<AnimationHandler>();
			playerCamera = PlayerCameraController.instance.GetComponentInChildren<Camera>().transform;
			jumpsRemaining = jumpsAllowed;
		}

		private void Update()
		{
			isGrounded = CheckIsGrounded();
			UpdateMovement();
			UpdateRotation();
		}

		private bool CheckIsGrounded()
		{
			Debug.DrawLine(groundCheckLocation.position, groundCheckLocation.position - new Vector3(0, 0.3f, 0),
				Color.red, 0.1f);
			if (!Physics.Raycast(groundCheckLocation.position, Vector3.down, 0.3f))
			{
				return false;
			}

			jumpsRemaining = jumpsAllowed;
			isJumping = false;
			return true;
		}


		public void HandleJump()
		{
			if (isGrounded)
			{
				Jump();
			}
			else if (isJumping && jumpsRemaining > 0)
			{
				Jump();
			}
		}

		private void Jump()
		{
			controller.Move(new Vector3(0, Mathf.Sqrt(jumpHeight*gravity*-1), 0));
			jumpsRemaining--;
			isJumping = true;
			animationHandler.PlayTargetAnimation("Jump");

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
			moveDirection = Vector3.zero;
			moveDirection = playerCamera.forward * inputHandler.verticalInput;
			moveDirection += playerCamera.right * inputHandler.horizontalInput;
			moveDirection.Normalize();
			moveDirection.y = 0;
			moveDirection *= moveSpeed;
			moveDirection *= Time.deltaTime;
			moveDirection.y += gravity * Time.deltaTime;
			if (isGrounded) moveDirection.y = -0.5f;
			animationHandler.UpdateLocomotion(Mathf.Clamp01(Mathf.Abs(inputHandler.horizontalInput) +
			                                                Mathf.Abs(inputHandler.verticalInput)));

			controller.Move(moveDirection);
		}
	}
}