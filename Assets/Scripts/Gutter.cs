using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gutter : MonoBehaviour {

    private Pinsetter pinsetter;

	// Use this for initialization
	void Start () {
        pinsetter = FindObjectOfType<Pinsetter>();
	}

	private void OnTriggerExit(Collider other)
	{
        if (other.gameObject.name == "Ball") {
            pinsetter.SetBallOutOfPlay();
        }
	}
}
