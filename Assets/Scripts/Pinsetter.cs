using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pinsetter : MonoBehaviour
{
    public GameObject pinSet;

    private ActionMaster actionMaster = new ActionMaster();
    private Animator animator;
    private PinCounter pinCounter;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        pinCounter = FindObjectOfType<PinCounter>();
    }

    //Calls animation to be triggered based off of roll results from Game Manager
    public void PerformAction(ActionMaster.Action action) {

        if (action == ActionMaster.Action.Tidy)
        {
            animator.SetTrigger("tidyTrigger");
        }

        else if (action == ActionMaster.Action.Reset)
        {
            animator.SetTrigger("resetTrigger");
            pinCounter.Reset();
        }

        else if (action == ActionMaster.Action.EndTurn)
        {
            animator.SetTrigger("resetTrigger");
            pinCounter.Reset();
        }

        else if (action == ActionMaster.Action.EndGame)
        {
            throw new UnityException("Don't know how to handle end game yet");
        }
    }

    // Raises pins during tidy/reset animations
    public void RaisePins()
    {
        foreach (Pin pin in FindObjectsOfType<Pin>())
        {
            pin.RaiseIfStanding();
        }
    }

    // Lowers pins during tidy/reset animations
    public void LowerPins() {

        foreach (Pin pin in FindObjectsOfType<Pin>())
        {
            pin.Lower();
        }
    }

    // Renews pins during reset
    public void RenewPins() {
        Debug.Log("Renew Pins");
        Instantiate(pinSet, new Vector3(0, 20, 1829), Quaternion.identity);
    }

    // Destroys pins that are knocked off the lane
	private void OnTriggerExit(Collider other)
	{
        GameObject pinLeaving = other.gameObject;

        if (pinLeaving.GetComponent<Pin>()) {
            Destroy(pinLeaving);
        }
	}

}