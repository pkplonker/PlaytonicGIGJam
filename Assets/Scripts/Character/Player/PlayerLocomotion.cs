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
        [SerializeField] private float jumpSpeed;

        private Vector3 moveDirection;
        private InputHandler inputHandler;
        private AnimationHandler animationHandler;
        private Transform playerCamera;
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
            UpdateMovement();
            UpdateRotation();
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
            float y = moveDirection.y;
            moveDirection *= moveSpeed * Time.deltaTime;
           // moveDirection.y = y;
            moveDirection.y +=gravity*Time.deltaTime;
            if (controller.isGrounded && inputHandler.inputA)
            {
                Debug.Log("jumping");
                moveDirection.y = jumpSpeed;
            }
            animationHandler.UpdateLocomotion(Mathf.Clamp01(Mathf.Abs(inputHandler.horizontalInput) + Mathf.Abs(inputHandler.verticalInput)));
            controller.Move(moveDirection);
        }
    }
}
