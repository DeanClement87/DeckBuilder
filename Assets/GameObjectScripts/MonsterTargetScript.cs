using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MonsterTargetScript : MonoBehaviour
{
    private GameManager gameManager;
    private ActionManager actionManager;

    private MonsterScript ms;
    private Image monsterImage;
    private bool canBeTargeted;
    void Awake()
    {
        gameManager = GameManager.Instance;
        actionManager = ActionManager.Instance;

        ms = GetComponent<MonsterScript>();
        monsterImage = GetComponent<Image>();
    }

    private void OnMouseDown()
    {
        if (gameManager.gameState != GameManager.GameState.CardAction) return;

        if (!canBeTargeted) return;

        gameManager.TargetedMonster = ms.MonsterModel;

        var actionExecute = ActionExecuteFactory.GetActionExecute(actionManager.ActiveAction.Action);
        actionExecute.Execute();
    }

    private void Update()
    {
        if (gameManager.gameState != GameManager.GameState.CardAction)
        {
            monsterImage.color = Color.white;
            return;
        }

        var heroLane = gameManager.HeroLanes.First(x => x.IsHeroHere(gameManager.ActiveHero));
        switch (actionManager.ActiveAction.Target)
        {
            case ActionTargetEnum.MonsterAnyLane:
                canBeTargeted = true;
                monsterImage.color = Color.white;
                return;
            case ActionTargetEnum.MonsterThisLane:
                if (heroLane.OppositeLane.MonsterModels.Contains(ms.MonsterModel))
                {
                    canBeTargeted = true;
                    monsterImage.color = Color.white;
                    return;
                }
                break;
            case ActionTargetEnum.MonsterOutsideLane:
                //if the monster is in the opposite lane to our hero, then do nothing
                if (heroLane.OppositeLane.MonsterModels.Contains(ms.MonsterModel)) break;

                canBeTargeted = true;
                monsterImage.color = Color.white;
                return;
        }

        //if cannot target by the end, then it is not a possible target
        monsterImage.color = new Color(0.435f, 0.353f, 0.353f);
        canBeTargeted = false;
    }
}
