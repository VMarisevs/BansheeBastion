using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    // allows to create only one instance of this class
    public static GameManager instance = null;

    // map generation script
    public MapManager mapScript;

    // game init
    void InitGame()
    {
        // clear from enemies
        // generate by level difficulty
        mapScript.SetupScene();
    }


    // on awake 
    void Awake()
    {
        // blocking to load more instances of this class
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        // running init scripts
        mapScript = GetComponent<MapManager>();
        InitGame();
    }


    public void GameOver()
    {
        enabled = false;
    }



}
