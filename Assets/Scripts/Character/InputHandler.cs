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

		#endregion

		private void OnEnable()
		{
			inputActions ??= new InputControls();
			inputActions.Enable();
			inputActions.Movement.Movement.performed += inputActions => movementInput = inputActions.ReadValue<Vector2>();
		}

		private void Update()
		{
		}

		private void LateUpdate()
		{
			
		}


		private void OnDisable()
		{
			inputActions.Disable();
		}
	}
}