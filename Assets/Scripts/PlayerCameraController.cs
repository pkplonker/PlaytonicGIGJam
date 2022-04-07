using System;
using System.Collections;
using System.Collections.Generic;
using Character;
using Character.Player;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
	#region Variables

	public static PlayerCameraController instance;
	[SerializeField] Transform targetTransform;
	[SerializeField] Transform cameraTransform;
	[SerializeField] Transform cameraPivotTransform;

	[Header("Camera Movement")] [SerializeField] [Range(2, 300)]
	float lookSpeed = 0.1f;

	[SerializeField] [Range(2, 300)] float pivotSpeed = 0.03f;
	[SerializeField] [Range(2, 300)] float followSpeed = 0.1f;
	[SerializeField] [Range(0.01f, 100f)] float pivotRangeUpper = 35f;
	[SerializeField] [Range(-0f, -100f)] float pivotRangeLower = -35f;
	private Vector3 cameraFollowVelocity;
	private float defaultPosition;
	private float pivotAngle;
	private float lookAngle;
	private InputHandler inputHandler;

	#endregion

	private void Awake()
	{
		if (instance == null) instance = this;
		else Destroy(gameObject);
		Cursor.visible = false;
		inputHandler = PlayerController.instance.GetComponent<InputHandler>();
		defaultPosition = cameraTransform.localPosition.z;
	}

	private void FixedUpdate()
	{
		FollowTarget();
		HandleCameraRotation(inputHandler.mouseX, inputHandler.mouseY);
	}

	public void FollowTarget()
	{
		Vector3 targetPosition = Vector3.SmoothDamp(transform.position, targetTransform.position,
			ref cameraFollowVelocity, Time.deltaTime / followSpeed);

		transform.position = targetPosition;
	}

	public void HandleCameraRotation(float mouseXInput, float mouseYInput)
	{
		lookAngle += mouseXInput * lookSpeed * Time.deltaTime;
		pivotAngle -= mouseYInput * pivotSpeed * Time.deltaTime;
		pivotAngle = Mathf.Clamp(pivotAngle, pivotRangeLower, pivotRangeUpper);

		//y rot
		Vector3 rotation = Vector3.zero;
		rotation.y = lookAngle;
		Quaternion targetRotation = Quaternion.Euler(rotation);
		transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, .2f);
		//x rot
		rotation = Vector3.zero;
		rotation.x = pivotAngle;

		targetRotation = Quaternion.Euler(rotation);
		cameraPivotTransform.localRotation = targetRotation;
	}
}