﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour {

    public float standingThreshold = 3f;
    public float distToRaise = 40F;

    private Rigidbody rigidBody;

    // Use this for initialization
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool IsStanding()
    {
        float tiltX = (transform.eulerAngles.x < 180f) ? transform.eulerAngles.x : 270 - transform.eulerAngles.x;
        float tiltZ = (transform.eulerAngles.z < 180f) ? transform.eulerAngles.z : 270 - transform.eulerAngles.z;

        if (tiltX > standingThreshold || tiltZ > standingThreshold)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    //Raises pins before swipe
    public void RaiseIfStanding() {
        if (IsStanding())
        {
            transform.Translate(0, distToRaise, 0, Space.World);
            transform.rotation = Quaternion.Euler(270f, 0, 0);
            rigidBody.useGravity = false;
        }
    }

    //Lowers pins after swipe
    public void Lower() {
        if (IsStanding())
        {
            transform.Translate(0, -distToRaise, 0, Space.World);
            rigidBody.useGravity = true;
        }
    }
}
