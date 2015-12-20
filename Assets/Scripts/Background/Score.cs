using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour {

    private static int _score = 0;

    public static void addScore(int score)
    {
        _score += score;
    }

    private int barWidth;
    private int barHeight;
    private int barX;
    private int barY;

    void OnGUI()
    {
        GUI.Box(new Rect(barX, barY, barWidth, barHeight), "Score: " + _score);
    }

    public void Start()
    {
        barWidth = 100;
        barHeight = 20;
        barX = 0;
        barY = 0;
    }
}
