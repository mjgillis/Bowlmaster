using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionMaster {

    public enum Action {Tidy, Reset, EndTurn, EndGame};

    private int[] bowls = new int[21]; // total max number of turns
    private int bowl = 1;  // start at bowl one

    public static Action NextAction (List<int> pinFalls) {
        ActionMaster am = new ActionMaster();
        Action currentAction = new Action();

        foreach (int pinFall in pinFalls) {
            currentAction = am.Bowl(pinFall);
        }

        return currentAction;
    }

    public Action Bowl(int pins) // TODO make private
    {
        // Checks to make sure # of pins bowled is between 0-10
        if (pins < 0 || pins > 10) {
            throw new UnityException("Invalid number of pins bowled:" + pins);    
        }

        // Storing scores in array
        bowls[bowl - 1] = pins;

        if (bowl == 21)
        {
            return Action.EndGame;
        }

        if (bowl >= 19 && pins == 10) {
            bowl++;
            return Action.Reset;
        }
        else if (bowl == 20) {
            bowl++;
            if (bowls[19-1] == 10 && bowls[20-1] == 0) {
                return Action.Tidy;
            } else if ((bowls[19-1] + bowls[20-1]) % 10 == 0) {
                return Action.Reset;
            } else if (Roll21Awarded()) {
                return Action.Tidy;
            } else {
                return Action.EndGame;
            }
        }

        else if (bowl == 20 && ! Roll21Awarded()) {
            return Action.EndGame;
        }

        // Returns tidy if bowled first roll of frame & != strike
        if (bowl % 2 != 0)
        {
            // Checks for strike
            if (pins == 10)
            {
                bowl += 2;
                return Action.EndTurn;
            } else {
                bowl += 1;
                return Action.Tidy;
            }
        }

        // Return EndTurn if rolled 2nd in frame
        else if (bowl % 2 == 0)
        {
            bowl += 1;
            return Action.EndTurn;
        }

        // Throws exception if can't pick up any of the cases
        throw new UnityException("Not sure what action to return");
    }

    private bool Roll21Awarded ()
    {
        return (bowls[19 - 1] + bowls[20 - 1] >= 10);
    }
}
