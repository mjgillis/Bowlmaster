using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private List<int> rolls = new List<int>();
    private Pinsetter pinSetter;
    private Ball ball;
    private ScoreDisplay scoreDisplay;

	// Use this for initialization
	void Start () {
        pinSetter = FindObjectOfType<Pinsetter>();
        ball = FindObjectOfType<Ball>();
        scoreDisplay = FindObjectOfType<ScoreDisplay>();
	}
	
	public void Bowl (int pinFall) {

        //Try catch used to fail gracefully if Pinsetter unable to reset pins
        try {
            rolls.Add(pinFall);
            ball.Reset();

            pinSetter.PerformAction(ActionMaster.NextAction(rolls));
        } catch {
            Debug.LogWarning("Something went wrong in Bowl()");
        }

        //Try catch used to fail gracefully if displaying score does not work
        try {
            scoreDisplay.FillRolls(rolls);
            scoreDisplay.FillFrames(ScoreMaster.ScoreCumulative(rolls));
        } catch {
            Debug.LogWarning("Something went wrong filling roll card");
        }

        
	}
}
