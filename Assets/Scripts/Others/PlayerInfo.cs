using System;
using UnityEngine;

[Serializable]
public class PlayerInfo
{
    public string name;
    public int score;

    public static PlayerInfo CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<PlayerInfo>(jsonString);
    }

}