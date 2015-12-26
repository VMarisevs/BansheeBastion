using UnityEngine;
using System.Collections;

public class GameButtons : MonoBehaviour {

    public GameObject _navbarCanvas;
    public GameObject _marketCanvas;

    public void Start()
    {
        _closeMarket();
    }

    public void _openMarket()
    {
        _navbarCanvas.SetActive(false);
        _marketCanvas.SetActive(true);
    }

    public void _closeMarket()
    {
        _navbarCanvas.SetActive(true);
        _marketCanvas.SetActive(false);
    }

    public void _cheat()
    {
        LevelManager.addScore(100);
    }

    public void _buyKing()
    {

        // if score more that price of item
        if (LevelManager.getScore() > 100)
        {
            _closeMarket();
        }
        
    }
}
