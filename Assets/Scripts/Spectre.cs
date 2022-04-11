using System.Collections;
using UnityEngine;


public class Spectre : MonoBehaviour
{
    [SerializeField] private Transform playerModel;
    [SerializeField][Range(0,1000f)] private float rotationSpeedLower;
    [SerializeField] [Range(0,1000f)] private float rotationSpeedUpper;
    [SerializeField] [Range(0.5f,5f)] private float changeTimeLower=0.75f;
    [SerializeField] [Range(0.5f,5f)]private float changeTimeUpper = 2f;
    [SerializeField] [Range(-1,1)]private float yPosLower = -0.35f;
    [SerializeField] [Range(-1,1)]private float yPosUpper = +0.35f;
     private float rotationSpeedCurrent;
     private bool isClockWise;
     private float currentYPosTarget;
    private Coroutine changeDelayCor;
    private void Start()
    {
        changeDelayCor = StartCoroutine(PositionChangeDelay());
    }

    private void Update()
    {
        transform.Rotate(isClockWise? playerModel.up : -playerModel.up ,rotationSpeedCurrent *Time.deltaTime);
        transform.localPosition = Vector3.Lerp(transform.localPosition, 
            new Vector3(transform.localPosition.x, currentYPosTarget, transform.localPosition.z), Time.deltaTime);
        //change rotaitonSpeed
        //change heihght
    }

    private IEnumerator PositionChangeDelay()
    {

        yield return new WaitForSeconds(Random.Range(changeTimeLower,changeTimeUpper));
        rotationSpeedCurrent = Random.Range(rotationSpeedLower, rotationSpeedUpper);
        changeDelayCor = StartCoroutine(PositionChangeDelay());
        isClockWise = Random.value>0.5;
        currentYPosTarget = Random.Range(yPosLower, yPosUpper);


    }
}
