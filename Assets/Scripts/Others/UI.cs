using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;


public class UI : MonoBehaviour {

    public GameObject[] menuCanvas;


    //top 10
    public GameObject[] playerTop;
    private PlayerTop top10;

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
            top10 = PlayerTop.CreateFromJSON(www.text);
            //print(top10.player1 + " " + top10.score1);
            displayTopPlayers();
        }
        else
        {
            Debug.Log("WWW Error: " + www.error);
        }
    }

    private void displayTopPlayers()
    {
        /*
            Unity doesn't supports native json parsing,
            so it causes some issues with arrays of objects
            this is temporary code that receives the top scores
        */
        playerTop[0].GetComponent<Text>().text = top10.player1 + " " + top10.score1;
        playerTop[1].GetComponent<Text>().text = top10.player2 + " " + top10.score2;
        playerTop[2].GetComponent<Text>().text = top10.player3 + " " + top10.score3;
        playerTop[3].GetComponent<Text>().text = top10.player4 + " " + top10.score4;

        //for (int i = 0; i < (playerTop.Length); i++)
        //{
        //    Text child = playerTop[i].GetComponent<Text>();
        //    child.text = top10.player1 + " " + top10.score1;
        //}
    }
}
