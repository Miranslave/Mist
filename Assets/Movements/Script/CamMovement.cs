using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMovement : MonoBehaviour
{


    private Vector3 offset = new Vector3(-7.5f, 5f, -10f);
    public float smoothTime = 0.05f;

    private Vector3 velocity = Vector3.zero;

    [SerializeField] private Transform target;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}


