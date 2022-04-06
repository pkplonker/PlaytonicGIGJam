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

        private InputHandler inputHandler;
        private AnimationHandler animationHandler;
        #endregion

        private void Awake()
        {
            inputHandler = GetComponent<InputHandler>();
            controller = GetComponent<CharacterController>();
            animationHandler = GetComponentInChildren<AnimationHandler>();
        }

        private void Update()
        {
            UpdateMovement();
            UpdateRotation();
        }

        private void UpdateRotation()
        {
            transform.Rotate(0,inputHandler.movementInput.x* rotationSpeed*Time.deltaTime,0);
        }

        private void UpdateMovement()
        {
            
            Vector3 moveDirection = new Vector3(0, 0, inputHandler.movementInput.y) ;
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= moveSpeed * Time.deltaTime;
            moveDirection.y += gravity * Time.deltaTime;
            controller.Move(moveDirection);
            animationHandler.UpdateLocomotion(moveDirection);
        }
    }
}
