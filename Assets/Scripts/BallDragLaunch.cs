using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Ball))]
public class BallDragLaunch : MonoBehaviour {
    
    private Ball ball;
    private Vector3 dragStart, dragEnd;
    private float timeStart, timeEnd;

	// Use this for initialization
	void Start () {
        ball = GetComponent<Ball>();
	}
	
    public void MoveStart(float amount) {
        
        if (! ball.inPlay)
        {
            ball.transform.Translate(new Vector3(amount, 0, 0));
        }
    }

    public void DragStart () {
        //Capture time & position of mouse click
        dragStart = Input.mousePosition;
        timeStart = Time.time;

    }

    public void DragEnd () {
        //Capture length of vector and time
        //Calls function to launch ball at the end, passing velocity
        dragEnd = Input.mousePosition;
        timeEnd = Time.time;

        float dragDuration = timeEnd - timeStart;

        float launchSpeedX = (dragEnd.x - dragStart.x) / dragDuration;
        float launchSpeedZ = (dragEnd.y - dragStart.y) / dragDuration;

        Vector3 launchVelocity = new Vector3 (launchSpeedX, 0f, launchSpeedZ);

        ball.Launch(launchVelocity);

    }
}
