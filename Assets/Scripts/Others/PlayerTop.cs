
using System;
using UnityEngine;

[Serializable]
public class PlayerTop{

    //public PlayerInfo[] players;
    public string player1;
    public int score1;
    public string player2;
    public int score2;
    public string player3;
    public int score3;
    public string player4;
    public int score4;
    public string player5;
    public int score5;
    public string player6;
    public int score6;
    public string player7;
    public int score7;
    public string player8;
    public int score8;
    public string player9;
    public int score9;
    public string player10;
    public int score10;

    public static PlayerTop CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<PlayerTop>(jsonString);
    }
}
