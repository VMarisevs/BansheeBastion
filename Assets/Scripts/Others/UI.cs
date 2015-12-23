using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
/*
Tools -> NutGet Packet Manager -> Manage Nut Get Packages for this Solution
Search for "newtonsoft json" and install it
This NutGet used to deserialize the Json, that received from web server 
*/
//using Newtonsoft.Json;
//using Pathfinding.Serialization.JsonFx;
//using JsonFX;
//using Owl.Converter;

public class UI : MonoBehaviour {

    public GameObject[] menuCanvas;
    //top 10
    Person[] persons = new Person[10];

    // for play button        
    public void LoadScene(int level)
    {
        SceneManager.LoadScene(level);
    }

    // quit game
    public void quitGame()
    {
        Application.Quit();
    }


    // switch between tabs
    public void _openScoresTab()
    {
        changeMenu(1);
    }

    public void _openSettingsTab()
    {
        changeMenu(2);
    }

    public void _openHelpTab()
    {
        changeMenu(3);
    }

    public void _openMainTab()
    {
        changeMenu(0);
    }

    private void changeMenu(int id)
    {
        for (int i = 0; i < menuCanvas.Length; i++)
        {
            menuCanvas[i].SetActive(false);
        }

        menuCanvas[id].SetActive(true);
    }


    public void Start()
    {
       _openMainTab();
        GetTop10();
    }

    private void GetTop10()
    {
        string url = "http://bansheebastion.vmarisevs.me/GetTop10.php";
        WWW www = new WWW(url);
        StartCoroutine(WaitForRequest(www));
    }

    IEnumerator WaitForRequest(WWW www)
    {
        yield return www;
        // check for errors
        if (www.error == null)
        {
            // Dictionary<string, object> search = Json.Deserialize(response) as Dictionary<string, object>;

             Debug.Log("WWW Ok!: " + www.data);

        }
        else
        {
            Debug.Log("WWW Error: " + www.error);
        }
    }
}
