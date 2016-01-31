using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;


public class UI : MonoBehaviour {

    public GameObject[] menuCanvas;


    //top 10
    public GameObject[] playerTop;
    private PlayerInfo[] top5 = new PlayerInfo[5];

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


    public void SubmitScore(string name, int score)
    {
        string url = "https://stalloodyinglasernfithea:a4db51d72cb61a6ab06080500637bf1631073267@vmarisevs.cloudant.com/bansheebastion";

        WWWForm form = new WWWForm();
        string postdata = "{\"name\":\"" + name + "\",\"score\":" + score + "}";
        byte[] data = System.Text.Encoding.UTF8.GetBytes(postdata);


        System.Collections.Generic.Dictionary<string,string> headers = form.headers;
        headers["Content-Type"] = "application/json";

        WWW www = new WWW(url, data, headers);


        StartCoroutine(WaitForRequest(www));

    }

    private IEnumerator WaitForRequest(WWW www)
    {
        yield return www;

        // check for errors
        if (www.error == null)
        {
            Debug.Log("WWW Ok!: " + www.text);
        }
        else {
            Debug.Log("WWW Error: " + www.error);
        }
    }

    public void Start()
    {
       _openMainTab();
        GetTop10();

        //SubmitScore("first", 234123);

    }

    private void GetTop10()
    {
        string url = "https://stalloodyinglasernfithea:a4db51d72cb61a6ab06080500637bf1631073267@vmarisevs.cloudant.com/bansheebastion/_design/toplist/_view/top5?limit=5&descending=true";
        WWW www = new WWW(url);
        StartCoroutine(WaitForRequestPlayerTop(www));
    }

    IEnumerator WaitForRequestPlayerTop(WWW www)
    {
        yield return www;
        // check for errors
        if (www.error == null)
        {
            int first = www.text.IndexOf("\"rows\":") + "\"rows\":".Length;
            int last = www.text.Length - 2 - first;
            string result = www.text.Substring(first, last);


            for (int i = 0; i < 5; i++)
            {
                int jsonStart = result.IndexOf("\"value\":") + "\"value\":".Length;
                int jsonEnd = result.IndexOf("}}") + 1;
                int jsonCount = jsonEnd - jsonStart;

                string playerStats = result.Substring(jsonStart, jsonCount);

                result = result.Substring(jsonEnd + "}}".Length + 1);

                top5[i] = PlayerInfo.CreateFromJSON(playerStats);
            }

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
        for (int i = 0; i < playerTop.Length; i++)
        {
            playerTop[i].GetComponent<Text>().text = top5[i].name + " " + top5[i].score;
        }

    }
}
