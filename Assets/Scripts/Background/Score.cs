using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour {

    private static int _score = 0;

    public static void addScore(int score)
    {
        _score += score;
    }


    void OnGUI()
    {
        displayScore();
    }

    private void displayScore()
    {
        MenuPositions mp = MenuPositions.getInstance();
        GUI.Box(mp.getScoreBox(), "Score: " + _score, mp.getBoxStyle());
        
    }

}
