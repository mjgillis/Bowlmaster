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
        List<int> frames = new List<int>();

        for (int i = 1; i < rolls.Count; i += 2) {

            if (frames.Count == 10) { break; }

            // Normal open frame
            if (rolls[i-1] + rolls[i] < 10) {
                frames.Add(rolls[i - 1] + rolls[i]);
            }

            if (rolls.Count - i <= 1) { break; }

            // Strike frame
            if (rolls[i-1] == 10) {
                i--;                                        // Strike frame has just one roll
                frames.Add(10 + rolls[i + 1] + rolls[i + 2]);
            }

            // Spare frame
            else if (rolls[i - 1] + rolls[i] == 10) {
                frames.Add(10 + rolls[i + 1]);
            }

        }


        return frames;
    }
}