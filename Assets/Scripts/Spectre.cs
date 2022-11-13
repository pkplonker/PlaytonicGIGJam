using System.Collections;
using UnityEngine;


public class Spectre : MonoBehaviour
{
	[SerializeField] private Transform playerModel;

	[SerializeField] private Transform targetPosition;
	[SerializeField] [Range(0, 1000f)] private float rotationSpeedLower;
	[SerializeField] [Range(0, 1000f)] private float rotationSpeedUpper;
	[SerializeField] [Range(0.5f, 5f)] private float changeTimeLower = 0.75f;
	[SerializeField] [Range(0.5f, 5f)] private float changeTimeUpper = 2f;
	[SerializeField] [Range(-1, 1)] private float yPosLower = -0.35f;
	[SerializeField] [Range(-1, 1)] private float yPosUpper = +0.35f;
	private float rotationSpeedCurrent;
	private bool isClockWise;
	private float currentYPosTarget;
	private Coroutine changeDelayCor;
	[SerializeField] private bool attack;
	[SerializeField] private float attackRotationSpeed = 100f;
	private void Start()
	{
		changeDelayCor = StartCoroutine(PositionChangeDelay());
	}

	private void Update()
	{
		if (!attack)
		{
			transform.localPosition = Vector3.Lerp(transform.localPosition,
				new Vector3(transform.localPosition.x, currentYPosTarget, transform.localPosition.z), Time.deltaTime);
		}
		else
		{
			transform.Rotate(-playerModel.up,attackRotationSpeed*Time.deltaTime);
		}
	}

	private IEnumerator PositionChangeDelay()
	{
		yield return new WaitForSeconds(Random.Range(changeTimeLower, changeTimeUpper));
		currentYPosTarget = Random.Range(yPosLower, yPosUpper);
		changeDelayCor = StartCoroutine(PositionChangeDelay());
	}
}