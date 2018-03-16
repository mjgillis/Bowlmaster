using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public Vector3 launchVelocity;
    public bool inPlay = false;

    private Rigidbody rigidBody;
    private AudioSource audioSource;
    private Vector3 ballStartPosition;

    // Use this for initialization
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.useGravity = false;

        ballStartPosition = transform.position;
    }

    public void Launch(Vector3 velocity)
    {
        if (! inPlay) {
            rigidBody.useGravity = true;
            rigidBody.velocity = velocity;

            audioSource = GetComponent<AudioSource>();
            audioSource.Play();
        }

        inPlay = true;

    }

	public void Reset()
	{
        inPlay = false;
        rigidBody.velocity = Vector3.zero;
        rigidBody.angularVelocity = Vector3.zero;
        rigidBody.useGravity = false;
        transform.position = ballStartPosition;
        transform.rotation = Quaternion.identity;
	}

}