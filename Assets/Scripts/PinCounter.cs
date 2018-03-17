using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinCounter : MonoBehaviour {

    public Text standingDisplay;

    private GameManager gameManager;
    private int lastStandingCount = -1;
    private float lastChangeTime;
    private int lastSettledCount = 10;
    private bool ballOutOfPlay = false;

	// Use this for initialization
	void Start () {
        gameManager = FindObjectOfType<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
        standingDisplay.text = CountStanding().ToString();

        if (ballOutOfPlay)
        {
            UpdateStandingCountAndSettle();
            standingDisplay.color = Color.red;
        }
	}

	public void Reset()
	{
        lastSettledCount = 10;
	}

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Ball")
        {
            ballOutOfPlay = true;
        }
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

        gameManager.Bowl(pinFall);

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

}
