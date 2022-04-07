using System;
using System.Collections;
using UnityEngine;

namespace Character.Player
{
	public class PlayerLocomotion : MonoBehaviour
	{
		#region Variables

		private CharacterController controller;


		[SerializeField] private float moveSpeed;
		[SerializeField] private float rotationSpeed;
		[SerializeField] private float jumpForce = 3f;
		[SerializeField] private float leapVelocity = 0.5f;
		[SerializeField] private float fallMultiplier = 1;


		[SerializeField] private Transform groundCheckLocation;
		private InputHandler inputHandler;
		private AnimationHandler animationHandler;
		private Transform playerCamera;
		private Vector3 moveDirection;
		private int jumpsRemaining = 0;
		[SerializeField] private int jumpsAllowed;
		private bool isJumping;
		private bool isGrounded;
		private Rigidbody rb;
		private Vector3 targetPosition;
		private Vector3 normalVector;
		private float fallSpeed;
		private float inAirTimer;
		private bool shouldJump;
		private float lastYVelocity;

		#endregion

		private void Awake()
		{
			inputHandler = GetComponent<InputHandler>();
			controller = GetComponent<CharacterController>();
			animationHandler = GetComponentInChildren<AnimationHandler>();
			playerCamera = PlayerCameraController.instance.GetComponentInChildren<Camera>().transform;
			jumpsRemaining = jumpsAllowed;
			rb = GetComponent<Rigidbody>();
		}

		private void FixedUpdate()
		{
			UpdateMovement();
			UpdateVertical();
			UpdateRotation();
			HandleJump();
			rb.velocity += new Vector3(0, lastYVelocity, 0);
			//lastYVelocity = rb.velocity.y;
		}

		private void UpdateVertical()
		{
			Debug.DrawLine(groundCheckLocation.position, groundCheckLocation.position - new Vector3(0, 0.4f, 0),
				Color.red, 0.1f);
			if (Physics.Raycast(groundCheckLocation.position, Vector3.down, out RaycastHit hit, 0.4f))
			{
				// on ground
				normalVector = hit.normal;
				jumpsRemaining = jumpsAllowed;
				isJumping = false;
				animationHandler.animator.SetBool("isJumping", false);
				animationHandler.animator.SetBool("isFalling", false);
				if (inAirTimer > 0.5f)
				{
					animationHandler.PlayTargetAnimation("Land");
				}

				inAirTimer = 0;
				isGrounded = true;
				lastYVelocity = 0;
				return;
			}

			isGrounded = false;
			inAirTimer += Time.deltaTime;
			if ((!animationHandler.animator.GetBool("isJumping") ||
			    !animationHandler.animator.GetBool("isFalling")) && inAirTimer>0.5f)
			{
				animationHandler.animator.SetBool("isFalling", true);
			}


			lastYVelocity += Physics.gravity.y* fallMultiplier;
			rb.AddForce(transform.forward * leapVelocity);
		}

		public void RequestJump()
		{
			shouldJump = true;
		}

		private void HandleJump()
		{
			if (!shouldJump) return;
			shouldJump = false;
			if (isGrounded || (isJumping && jumpsRemaining > 0))
			{
				Jump();
			}
			
		}

		private void Jump()
		{
			Debug.Log("Jumping");
			//rb.velocity += Vector3.up * jumpForce; 
			lastYVelocity +=jumpForce;
			animationHandler.animator.SetBool("isJumping", true);
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
			moveDirection.y = 0;
			moveDirection *= moveSpeed;
			moveDirection = Vector3.ProjectOnPlane(moveDirection, normalVector);
			rb.velocity = moveDirection;
			rb.velocity = new Vector3(rb.velocity.x, lastYVelocity, rb.velocity.z);
			animationHandler.UpdateLocomotion(Mathf.Clamp01(Mathf.Abs(inputHandler.verticalInput) +
			                                                Mathf.Abs(inputHandler.horizontalInput)));
		}
	}
}