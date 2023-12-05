using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class StartGameScript : MonoBehaviour, IPointerClickHandler
{
    private GameManager gameManager;
    void Awake()
    {
        gameManager = GameManager.Instance;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (gameManager.HeroesSelectionFromMainScreen.Count() == 4)
            SceneManager.LoadScene("GameScene");
    }
}
