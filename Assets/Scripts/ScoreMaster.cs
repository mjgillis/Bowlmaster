using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class ScoreMaster {

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
        List<int> frames = new List<int>();

        // Goes through every individual roll, second i goes through second roll
        for (int i = 1; i < rolls.Count; i += 2) {

            if (frames.Count == 10) { break; } // Ensures no 11th frame score

            // Normal open frame
            if (rolls[i-1] + rolls[i] < 10) {
                frames.Add(rolls[i - 1] + rolls[i]);
            }

            if (rolls.Count - i <= 1) { break; } // Ensures you can look at least 1 roll ahead

            // Strike frame
            if (rolls[i-1] == 10) {
                i--;                                            // Strike frame has just one roll
                frames.Add(10 + rolls[i + 1] + rolls[i + 2]);   // Strike bonus calculation
            }

            // Spare frame
            else if (rolls[i - 1] + rolls[i] == 10) {
                frames.Add(10 + rolls[i + 1]);                  // Spare bonus calculation
            }

        }


        return frames;
    }
}