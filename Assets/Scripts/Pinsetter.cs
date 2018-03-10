using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pinsetter : MonoBehaviour
{
    public Text standingDisplay;
    public GameObject pinSet;

    private bool ballOutOfPlay = false;
    private int lastStandingCount = -1;
    private float lastChangeTime;
    private int lastSettledCount = 10;
    private ActionMaster actionMaster = new ActionMaster();

    private Ball ball;
    private Animator animator;

    // Use this for initialization
    void Start()
    {
        ball = FindObjectOfType<Ball>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        standingDisplay.text = CountStanding().ToString();

        if (ballOutOfPlay)
        {
            UpdateStandingCountAndSettle();
            standingDisplay.color = Color.red;
        }
    }

    public void SetBallOutOfPlay () {
        ballOutOfPlay = true;
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

    // Update the lastStandingCount
    void UpdateStandingCountAndSettle()
    {
        int currentStanding = CountStanding();

        // Checks if count changed and records time if changed
        if (currentStanding != lastStandingCount)
        {
            lastChangeTime = Time.time;
            lastStandingCount = currentStanding;
            return;
        }

        float settleTime = 3f;  // How long to wait to consider pins settled

        // Compares current time to when last pin count changed and settles pins if greater than settleTime;
        if ((Time.time - lastChangeTime) > settleTime)
        { // If last change > 3s ago
            PinsHaveSettled();
        }

    }

    // Settles pins, ending the roll and reseting the lane for the next roll
    void PinsHaveSettled()
    {
        int standing = CountStanding();
        int pinFall = lastSettledCount - standing;
        lastSettledCount = standing;

        ActionMaster.Action action = actionMaster.Bowl(pinFall);

        if (action == ActionMaster.Action.Tidy) {
            animator.SetTrigger("tidyTrigger");
        }

        else if (action == ActionMaster.Action.Reset) {
            animator.SetTrigger("resetTrigger");
            lastSettledCount = 10;
        }

        else if (action == ActionMaster.Action.EndTurn)
        {
            animator.SetTrigger("resetTrigger");
            lastSettledCount = 10;
        }

        else if (action == ActionMaster.Action.EndGame)
        {
            throw new UnityException("Don't know how to handle end game yet");
        }

        ball.Reset();
        lastStandingCount = -1; // Indicates pins have settled, and ball not back in box
        ballOutOfPlay = false;
        standingDisplay.color = Color.green;
    }

    // Counts the standing pins and is called everytime the system checks if something is standing
    // This occurs every update frame once the ball enters the trigger box and stops being called when pins settle
    int CountStanding()
    {
        int standing = 0;

        foreach (Pin pin in FindObjectsOfType<Pin>())
        {
            if (pin.IsStanding())
            {
                standing++;
            }
        }

        return standing;
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