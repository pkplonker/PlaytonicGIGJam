using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    #region Variables

    public static PlayerCameraController instance;
    [SerializeField] private Transform target;
    private Vector3 cameraPositionOffset;


    #endregion

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        Cursor.visible = false;
    }

    private void LateUpdate()
    {
        
    }
}
