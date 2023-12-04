using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class ConfirmHeroPlacementScript : MonoBehaviour, IPointerDownHandler
{
    private GameManager gameManager;

    void Awake()
    {
        gameManager = GameManager.Instance;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //count all the heroes in all the lanes
        var heroesInLanesCount = 0;
        foreach (var lane in gameManager.HeroLanes)
            heroesInLanesCount += lane.ObjectsInLane.Count();

        //if the count is not equal to total heroes then not all heroes have been placed.
        if (heroesInLanesCount != gameManager.Heroes.Count(x => x.Dead == false))
        {
            Debug.Log("Cannot end hero placement, not all heroes have been placed in a lane.");
            return;
        }

        gameManager.HideGameObjectOffScreen("HeroTray", true);
        gameManager.HideGameObjectOffScreen("ConfirmHeroPlacement", true);
        gameManager.ChangeGameState(GameManager.GameState.MonsterSpawn);
    }
}
