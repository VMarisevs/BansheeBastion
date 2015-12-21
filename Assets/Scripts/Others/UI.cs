using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour {
    private SpriteRenderer buttonSr;

    private Sprite[] uiSprites;

    public void Awake()
    {
        uiSprites = Resources.LoadAll<Sprite>("Sprites/UI");
       // print(uiSprites[0]);
        print(uiSprites.Length);
    }



    public void LoadScene(int level)
    {
        SceneManager.LoadScene(level);
    }

}
