using System;
using System.Collections;
using UnityEngine;

namespace Character.Player
{
	public class PlayerLocomotion : MonoBehaviour
	{
		#region Variables

		private CharacterController controller;

		[Header("Movement Params")] [Range(0, 20)] [SerializeField]
		private float moveSpeed;

		[Range(0, 40)] [SerializeField] private float rotationSpeed;

		[Range(0, 20)] [SerializeField] private float airSpeed = 4;

		[Header("Support Transforms")] [SerializeField]
		private Transform forwardCheckLocation;

		[SerializeField] private Transform groundCheckLocation;

		[Header("Gravity & Jump")] [Range(0, 20)] [SerializeField]
		private float jumpForce = 3f;

		[Range(0, 20)] [SerializeField] private float secondaryJumpForce = 4.5f;

		[Range(0, 200)] [SerializeField] private float leapVelocity = 100f;
		[Range(0, 1)] [SerializeField] private float fallMultiplier = 1;
		[SerializeField] private int jumpsAllowed;
		[SerializeField] private LayerMask groundLayer;
		private InputHandler inputHandler;
		private AnimationHandler animationHandler;
		private Transform playerCamera;
		private Rigidbody rb;
		private CharacterStats stats;

		private Vector3 moveDirection;
		private Vector3 targetPosition;
		private Vector3 normalVector;
		private bool isJumping;
		private bool isGrounded;
		private bool shouldJump;
		private float fallSpeed;
		private float inAirTimer;
		private float lastYVelocity;
		private int jumpsRemaining = 0;
		private static readonly int IsJumping = Animator.StringToHash("isJumping");
		private static readonly int IsFalling = Animator.StringToHash("isFalling");
		public event Action OnLand;
		public event Action OnJump;
		public event Action<bool> OnGroundedChange;

		#endregion

		private void Awake()
		{
			inputHandler = GetComponent<InputHandler>();
			controller = GetComponent<CharacterController>();
			animationHandler = GetComponentInChildren<AnimationHandler>();
			playerCamera = PlayerCameraController.instance.GetComponentInChildren<Camera>().transform;
			jumpsRemaining = jumpsAllowed;
			rb = GetComponent<Rigidbody>();
			stats = GetComponent<CharacterStats>();
		}

		private void FixedUpdate()
		{
			if (stats.canMove)
			{
				UpdateMovement();
				UpdateVertical();
				UpdateRotation();
				HandleJump();
				rb.velocity += new Vector3(0, lastYVelocity, 0);
			}
			else
			{
				animationHandler.UpdateLocomotion(0);
				animationHandler.animator.Play("Empty");
				rb.velocity = Vector3.zero;
			}
			
		}

		private void UpdateVertical()
		{
			Debug.DrawLine(groundCheckLocation.position, groundCheckLocation.position - new Vector3(0, 0.4f, 0),
				Color.red, 0.1f);
			if (Physics.Raycast(groundCheckLocation.position, Vector3.down, out RaycastHit hit, 0.4f, groundLayer ))
			{
				// on ground
				OnGroundedChange?.Invoke(true);
				normalVector = hit.normal;
				jumpsRemaining = jumpsAllowed;
				isJumping = false;
				animationHandler.animator.SetBool(IsJumping, false);
				animationHandler.animator.SetBool(IsFalling, false);
				if (inAirTimer > 0.3f)
				{
					animationHandler.PlayTargetAnimation("Land");
					OnLand?.Invoke();
				}

				inAirTimer = 0;
				isGrounded = true;
				lastYVelocity = 0;
				return;
			}

			OnGroundedChange?.Invoke(false);

			isGrounded = false;
			inAirTimer += Time.deltaTime;
			if ((!animationHandler.animator.GetBool(IsJumping) ||
			     !animationHandler.animator.GetBool(IsFalling)) && inAirTimer > 0.5f)
			{
				animationHandler.animator.SetBool(IsFalling, true);
			}


			lastYVelocity += Physics.gravity.y * fallMultiplier;
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
			lastYVelocity = -1;
			if (jumpsRemaining < jumpsAllowed)
			{
				lastYVelocity += secondaryJumpForce;
			}
			else
			{
				lastYVelocity += jumpForce;
			}

			jumpsRemaining--;
			isJumping = true;
			OnJump?.Invoke();

			Debug.DrawRay(forwardCheckLocation.position, transform.forward * 0.4f, Color.red, 0.1f);
			if (Physics.Raycast(forwardCheckLocation.position, transform.forward, 0.4f))
			{
				rb.velocity = new Vector3(0, lastYVelocity, 0);
				animationHandler.UpdateLocomotion(0);
			}

			if (!animationHandler.animator.GetBool(IsJumping))
			{
				animationHandler.animator.SetBool(IsJumping, true);
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
			moveDirection.y = -0.5f;
			if (Physics.Raycast(groundCheckLocation.position, Vector3.down, out RaycastHit hit, 0.4f))
			{
				moveDirection *= moveSpeed;
			}
			else
			{
				moveDirection *= airSpeed;
			}

			moveDirection = Vector3.ProjectOnPlane(moveDirection, normalVector);
			Debug.DrawRay(forwardCheckLocation.position, transform.forward * 0.4f, Color.red, 0.1f);
			rb.velocity = new Vector3(moveDirection.x, lastYVelocity, moveDirection.z);
			animationHandler.UpdateLocomotion(Mathf.Clamp01(Mathf.Abs(inputHandler.verticalInput) +
			                                                Mathf.Abs(inputHandler.horizontalInput)));
		}
	}
}