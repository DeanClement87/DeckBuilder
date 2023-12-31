using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HeroTargetScript : MonoBehaviour, IPointerClickHandler
{
    private GameManager gameManager;
    private ActionManager actionManager;
    private MonsterManager monsterManager;

    private HeroScript hs;
    private Image heroImage;
    private bool canBeTargeted;

    void Awake()
    {
        gameManager = GameManager.Instance;
        actionManager = ActionManager.Instance;
        monsterManager = MonsterManager.Instance;

        hs = GetComponent<HeroScript>();
        heroImage = GetComponent<Image>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!canBeTargeted) return;

        if (gameManager.gameState == GameManager.GameState.CardAction)
        {
            gameManager.TargetedHero = hs.HeroModel;

            var actionExecute = ActionExecuteFactory.GetActionExecute(actionManager.ActiveAction.Action);
            actionExecute.Execute();
        }

        if (gameManager.gameState == GameManager.GameState.MonsterTurn)
        {
            //DISTRACT
            var monsterAttack = monsterManager.monsterTurn.monster.BaseMonster.Attack - monsterManager.monsterTurn.monster.Distract;
            if (monsterAttack <= 0) monsterAttack = 0;

            //BLOCK
            if (hs.HeroModel.Block > 0)
            {
                if (monsterAttack > hs.HeroModel.Block)
                {
                    monsterAttack -= hs.HeroModel.Block;
                    hs.HeroModel.Block = 0;
                }
                else if (hs.HeroModel.Block >= monsterAttack)
                {
                    hs.HeroModel.Block -= monsterAttack;
                    monsterAttack = 0;
                }
            }

            //THORNS
            if (hs.HeroModel.Thorns > 0)
            {
                monsterManager.monsterTurn.monster.CurrentHealth -= hs.HeroModel.Thorns;
            }

            //BE AFRAID
            if (hs.HeroModel.BeAfraid)
            {
                monsterManager.monsterTurn.monster.CurrentHealth -= gameManager.Town.Fear()*2;
            }

            //BLOODTHIRSTY
            if (monsterManager.monsterTurn.monster.BaseMonster.MonsterAttributes.Contains(MonsterAttributeEnum.Bloodthirsty))
            {
                if (hs.HeroModel.Health < (hs.HeroModel.BaseHealth / 2))
                    monsterAttack = monsterAttack * 2;
            }

            //MONSTER ATTACK
            hs.HeroModel.Health -= monsterAttack;

            monsterManager.monsterCounter += 1;
            monsterManager.MonsterExecutor();
        }
    }

    private void Update()
    {
        if (gameManager.gameState == GameManager.GameState.CardAction)
        {
            var activeHeroLane = gameManager.HeroLanes.First(x => x.IsHeroHere(gameManager.ActiveHero));
            switch (actionManager.ActiveAction.Target)
            {
                case ActionTargetEnum.Self:
                    if (gameManager.ActiveHero == hs.HeroModel)
                    {
                        canBeTargeted = true;
                        heroImage.color = Color.white;
                        return;
                    }
                    break;
                case ActionTargetEnum.HeroAnyLane:
                    canBeTargeted = true;
                    heroImage.color = Color.white;
                    return;
                case ActionTargetEnum.HeroThisLane:
                    if (activeHeroLane.HeroesModels.Contains(hs.HeroModel))
                    {
                        canBeTargeted = true;
                        heroImage.color = Color.white;
                        return;
                    }
                    break;
                case ActionTargetEnum.HeroOutsideLane:
                    //if the monster is in the opposite lane to our hero, then do nothing
                    if (activeHeroLane.HeroesModels.Contains(hs.HeroModel)) break;

                    canBeTargeted = true;
                    heroImage.color = Color.white;
                    return;
            }

            //if cannot target by the end, then it is not a possible target
            heroImage.color = new Color(0.435f, 0.353f, 0.353f);
            canBeTargeted = false;
        }
        else
        {
            heroImage.color = Color.white;
            canBeTargeted = false;
        }

        if (gameManager.gameState == GameManager.GameState.MonsterTurn)
        {
            //get the lane with this hero in it, then if its lane number matches the monster lane number
            var heroLane = gameManager.HeroLanes.First(x => x.IsHeroHere(hs.HeroModel));
            if (monsterManager.monsterTurn.laneNumber == heroLane.laneNumber)
            {
                if (monsterManager.monsterTurn.monster.BaseMonster.MonsterAttributes.Contains(MonsterAttributeEnum.Sneaky))
                {
                    //find the hero with the lowest health in lane, if it this hero then light it up
                    var heroWithLowestHealth = heroLane.HeroesModels.OrderBy(hero => hero.Health).FirstOrDefault();
                    if (hs.HeroModel == heroWithLowestHealth)
                        canBeTargeted = true;
                    else
                        heroImage.color = new Color(0.435f, 0.353f, 0.353f);
                }
                else
                {
                    canBeTargeted = true;
                }
            }
            else //this hero is not in this lane
            {
                heroImage.color = new Color(0.435f, 0.353f, 0.353f);
            }
        }
    }
}
