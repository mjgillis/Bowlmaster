using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ScoreMaster {

    //Returns a list of cumulative scores, like a normal scorecard
    public static List<int> ScoreCumulative (List<int> rolls)
    {
        List<int> cumulativeScores = new List<int>();
        int runningTotal = 0;

        foreach (int framescore in ScoreFrames(rolls)) {
            runningTotal += framescore;
            cumulativeScores.Add(runningTotal);
        }

        return cumulativeScores;
    }

    //Return a list of individual frame scores, not cumulative like a scorecard
    public static List<int> ScoreFrames (List<int> rolls) 
    {
        //Returns individual frames
        List<int> frameList = new List<int>();
        int numRolls = rolls.Count;
        int currentFrameScore = 0;
        int currentFrameRollNum = 1; // if 1 it is first roll of turn, if 2 it is second roll
        bool nextFrame = false;
        int strike = 0;
        bool spare = false;

        // CALCULATE SCORES FOR EACH FRAME
        // ------------------------------------------------------
        // This loop goes through every roll in the list
        for (int i = 0; i < numRolls; i++)
        {

            // FIRST ROLL LOGIC - currenFrameRollNum must be odd
            // -------------------------------------------------
            if (currentFrameRollNum % 2 != 0)
            {
                if (strike > 0) {
                    strike--;

                    if (strike % 2 == 0 && frameList.Count <= 9) {
                        currentFrameScore += rolls[i];
                        frameList.Add(currentFrameScore);
                        currentFrameScore -= 10;

                        if (rolls[i] == 10) {
                            currentFrameScore -= 10;
                            strike++;
                        }
                    }
                }

                // Checks for strike and existing strike
                if (rolls[i] == 10)
                {
                    currentFrameScore += rolls[i];
                    strike += 2;
                }

                // Sets current frame score equal to roll as long as it isn't a strike
                else if (strike <= 1)
                {
                    currentFrameScore += rolls[i];
                }

            }

            // SECOND ROLL LOGIC - currentFramRollNum must be even
            // ---------------------------------------------------
            else if (currentFrameRollNum % 2 == 0)
            {
                if (strike > 0)
                {
                    strike--;

                    if (strike == 0 || strike % 2 != 0 && frameList.Count <= 9)
                    {
                        currentFrameScore += rolls[i];
                        frameList.Add(currentFrameScore);
                        currentFrameScore -= 10;
                        nextFrame = true;
                        strike = 0;
                    }
                }

                //Checks for spare
                else if ((currentFrameScore + rolls[i]) == 10)
                {
                    currentFrameScore += rolls[i];
                    nextFrame = true;
                    spare = true;
                }

                else
                {
                    currentFrameScore += rolls[i];
                    nextFrame = true;
                }

                //Checks for spare after strike
                if (currentFrameScore == 10)
                {
                    nextFrame = true;
                    spare = true;
                }
            }

            //POSTING SCORES
            // ---------------------------------------------

            // Adds score to frame list if no strike or spare
            if (nextFrame && !spare && frameList.Count <= 9) {
                frameList.Add(currentFrameScore);
                currentFrameScore = 0;
            }

            // Adds score to frame list with spare bonus
            if (spare && !nextFrame && frameList.Count <= 9) {
                frameList.Add(currentFrameScore);
                currentFrameScore -= 10;
                spare = false;
            }

            if (currentFrameRollNum % 2 != 0 && rolls[i] == 10) {
                currentFrameRollNum--;
            }

            currentFrameRollNum++;
            nextFrame = false;
        }

        return frameList;
    }
}
