using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShowHeroSelectionScreen : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        var backgroundBox = GameObject.Find("BackgroundBox");
        var mm = backgroundBox.GetComponent<MainMenuScreensScript>();

        mm.SetScreen("HeroSelect");

    }
}
