using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuScreensScript : MonoBehaviour
{
    GameObject mainMenuScreen;
    GameObject heroSelectScreen;

    void Awake()
    {
        mainMenuScreen = GameObject.Find("MainMenu");
        heroSelectScreen = GameObject.Find("HeroSelect");

        SetScreen("MainMenu");
    }

    public void SetScreen(string screen)
    {
        mainMenuScreen.SetActive(false);
        heroSelectScreen.SetActive(false);

        switch (screen)
        {
            case "MainMenu":
                mainMenuScreen.SetActive(true);
                break;

            case "HeroSelect":
                heroSelectScreen.SetActive(true);
                break;
        }

    }
}
