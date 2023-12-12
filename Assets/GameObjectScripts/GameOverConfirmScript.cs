using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameOverConfirmScript : MonoBehaviour, IPointerClickHandler
{
    private GameManager gameManager;

    void Awake()
    {
        gameManager = GameManager.Instance;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        var gameOverScreen = GameObject.Find($"GameOverScreen");
        var s = gameOverScreen.GetComponent<GameOverScreenScript>();
        s.ConfirmGameOver();

    }
}
