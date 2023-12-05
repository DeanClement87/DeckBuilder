using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EndHeroTurnScript : MonoBehaviour, IPointerClickHandler
{
    private GameManager gameManager;

    void Awake()
    {
        gameManager = GameManager.Instance;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        var endHeroTurn = GameObject.Find("EndHeroTurn");
        endHeroTurn.SetActive(false);

        gameManager.ChangeGameState(GameManager.GameState.MonsterTurn);
    }
}
