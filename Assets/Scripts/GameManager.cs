using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private List<int> bowls = new List<int>();
    private Pinsetter pinSetter;
    private Ball ball;

	// Use this for initialization
	void Start () {
        pinSetter = FindObjectOfType<Pinsetter>();
        ball = FindObjectOfType<Ball>();
	}
	
	public void Bowl (int pinFall) {

        bowls.Add(pinFall);
        ActionMaster.Action nextAction = ActionMaster.NextAction(bowls);
        pinSetter.PerformAction(nextAction);
        ball.Reset();
	}
}
