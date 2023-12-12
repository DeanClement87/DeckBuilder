using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LevelCompleteConfirmScript : MonoBehaviour, IPointerClickHandler
{
    private GameManager gameManager;

    void Awake()
    {
        gameManager = GameManager.Instance;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        var levelCompleteScreen = GameObject.Find($"LevelCompleteScreen");
        var s = levelCompleteScreen.GetComponent<LevelCompleteScreenScript>();
        s.ConfirmLevelComplete();
    }
}
