using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spectre : MonoBehaviour
{
    [SerializeField] private Transform playerModel;
    [SerializeField] private float rotationSpeed;
    private void Update()
    {
        transform.Rotate(playerModel.up,rotationSpeed*Time.deltaTime);
        //change rotaitonSpeed
        //change heihght
    }
}
