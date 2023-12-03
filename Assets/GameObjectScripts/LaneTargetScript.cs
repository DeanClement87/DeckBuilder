using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;

public class LaneTargetScript : MonoBehaviour, IPointerClickHandler
{
    private GameManager gameManager;
    private ActionManager actionManager;

    private LaneScript ls;
    private Image laneImage;
    private bool canBeTargeted;
    void Awake()
    {
        gameManager = GameManager.Instance;
        actionManager = ActionManager.Instance;

        ls = GetComponent<LaneScript>();
        laneImage = GetComponent<Image>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (gameManager.gameState != GameManager.GameState.CardAction) return;

        if (!canBeTargeted) return;

        gameManager.TargetedLane = gameManager.HeroLanes.First(x => x.laneNumber == ls.laneNumber);

        var actionExecute = ActionExecuteFactory.GetActionExecute(actionManager.ActiveAction.Action);
        actionExecute.Execute();
    }

    private void Update()
    {
        if (gameManager.gameState != GameManager.GameState.CardAction)
        {
            laneImage.color = Color.white;
            return;
        }

        switch (actionManager.ActiveAction.Target)
        {
            case ActionTargetEnum.ActiveHeroLane:
                var lane = gameManager.HeroLanes.First(x => x.IsHeroHere(gameManager.ActiveHero));
                if (lane.laneNumber == ls.laneNumber)
                {
                    foreach(var heroGameObject in lane.ObjectsInLane)
                    {
                        var i = heroGameObject.GetComponent<Image>();
                        i.raycastTarget = false;
                    }

                    canBeTargeted = true;
                    laneImage.color = new Color(208f / 255f, 222f / 255f, 148f / 255f, 1f);
                    return;
                }

                break;
            case ActionTargetEnum.OutsideHeroLane:
                var lanex = gameManager.HeroLanes.First(x => x.IsHeroHere(gameManager.ActiveHero));
                if (lanex.laneNumber != ls.laneNumber)
                {
                    var currentLane = gameManager.HeroLanes.First(x => x.laneNumber == ls.laneNumber);
                    foreach (var heroGameObject in currentLane.ObjectsInLane)
                    {
                        var i = heroGameObject.GetComponent<Image>();
                        i.raycastTarget = false;
                    }

                    canBeTargeted = true;
                    laneImage.color = new Color(208f / 255f, 222f / 255f, 148f / 255f, 1f);
                    return;
                }

                break;
        }

        canBeTargeted = false;
        laneImage.color = Color.white;
    }
}
