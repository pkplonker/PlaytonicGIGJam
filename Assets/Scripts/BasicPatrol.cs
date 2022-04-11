using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicPatrol : MonoBehaviour
{
    [SerializeField] private Transform patrolPointContainer;
    [SerializeField] private Transform model;
    private int index;
    [SerializeField] private float moveSpeed;
    private Rigidbody rb;
    private Transform[] patrolPoints;

    private void Start()
    {
        patrolPoints = patrolPointContainer.GetComponentsInChildren<Transform>();
    }

    private void Update()
    {
        if (Vector3.Distance(model.position, patrolPoints[index].position) < 0.2f)
        {
            UpdateTarget();
            return;
        }
        model.position = Vector3.MoveTowards(model.position, patrolPoints[index].position, moveSpeed/30000 / Time.deltaTime);

    }

    private void UpdateTarget()
    {
        index++;
        if (index > patrolPoints.Length - 1)
        {
            index = 0;
        }
    }
}
