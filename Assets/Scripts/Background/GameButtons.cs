using UnityEngine;
using System.Collections;

public class GameButtons : MonoBehaviour {

	
    public void OnGUI()
    {
        ShowMenu();
    }

    private void ShowMenu()
    {
        printStoreButton();
    }

    private void printStoreButton()
    {
        MenuPositions mp = MenuPositions.getInstance();

        if (GUI.Button(mp.getStoreButton(), "Store", mp.getButtonStyle()))
        {
            Store.showStore();
        }

        if (GUI.Button(mp.getCheatButton(), "Cheat", mp.getButtonStyle()))
        {
            Score.addScore(100);
        }
    }
}
