using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour {

    public Text[] rollTexts, frameTexts; 

    public void FillRolls (List<int> rolls) {

        //Grabs individual roll scores and places to string and shows in UI
        string scoresString = FormatRolls(rolls);

        for (int i = 0; i < scoresString.Length; i++)
        {
            rollTexts[i].text = scoresString[i].ToString();
        }
    }

    public void FillFrames (List<int> frames) {
       
        // Grabs cumulative frame score and displays in UI
        for (int i = 0; i < frames.Count; i++) {
            frameTexts[i].text = frames[i].ToString();
        }
    }

    public static string FormatRolls (List<int> rolls) {

        string output = "";

        for (int i = 0; i < rolls.Count; i++) {

            int box = output.Length + 1;

            //Handles spare scenario
            if ((box % 2 == 0 || box == 21) && rolls[i-1] + rolls[i] == 10) {
                output += "/";
            }


            // Handles strike in 10th frame
            else if (box >= 19 && rolls[i] == 10) {
                output += "X";
            }

            // Handles strike in frames 1-9
            else if (rolls[i] == 10) {
                output += "X ";
            }

            // Handles gutter ball
            else if (rolls[i] == 0) {
                output += "-";
            }

            // Handles normal role
            else {
                output += rolls[i].ToString();
            }
        }

        return output;
    }
}
