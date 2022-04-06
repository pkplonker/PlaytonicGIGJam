using System;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Character
{
	public class InputHandler : MonoBehaviour
	{
		#region Variables

		private InputControls inputActions;
		public Vector2 movementInput;
		public float verticalInput{get; private set; }
		public float horizontalInput{get; private set; }
		public float mouseX {get; private set; }
		public float mouseY {get; private set; }
		private Vector2 cameraInput;

		public bool inputA { get; private set; }
		public bool inputY { get; private set; }
		public bool inputX { get; private set; }
		public bool inputB { get; private set; }

		#endregion

		private void OnEnable()
		{
			inputActions ??= new InputControls();
			inputActions.Enable();
			inputActions.Movement.Movement.performed +=
				inputActions => movementInput = inputActions.ReadValue<Vector2>();
			inputActions.Movement.Camera.performed +=
				inputActions => cameraInput = inputActions.ReadValue<Vector2>();
			inputActions.Actions.A.performed += inputActions => inputA = true;
		}

		private void OnDisable()
		{
			inputActions.Disable();
		}

		private void Update()
		{
			MovementInput();
		}

		private void LateUpdate()
		{
			ResetInputs();
		}

		private void MovementInput()
		{
			verticalInput = movementInput.y;
			horizontalInput = movementInput.x;
			mouseX = cameraInput.x;
			mouseY = cameraInput.y;
		}

		private void ResetInputs()
		{
			inputA = false;
			inputY = false;
			inputX = false;
			inputB = false;
		}
	}
}