using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour {
    //private SpriteRenderer buttonSr;

    //private Sprite[] uiSprites;

    //public void Awake()
    //{
    //   // uiSprites = Resources.LoadAll<Sprite>("Sprites/UI");
    //   //// print(uiSprites[0]);
    //   // print(uiSprites.Length);
    //}

    public GameObject[] menuCanvas;
    //public bool[] menu = new bool[] { true, false }; 
            
    public void LoadScene(int level)
    {
        SceneManager.LoadScene(level);
    }

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
    }
}
