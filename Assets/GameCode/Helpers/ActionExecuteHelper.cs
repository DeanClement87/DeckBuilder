using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public static class ActionExecuteHelper
{
    public static void EndOfExecute()
    {
        var gameManager = GameManager.Instance;
        var actionManager = ActionManager.Instance;

        if (gameManager.TargetedLane != null)
        {
            foreach (var lane in gameManager.HeroLanes)
            {
                foreach(var heroGameObject in lane.ObjectsInLane)
                {
                    var i = heroGameObject.GetComponent<Image>();
                    i.raycastTarget = true;
                }
            }
        }

        //apply the damage.
        if (actionManager.IncomingDamage > 0 && gameManager.TargetedMonster != null)
        {
            gameManager.TargetedMonster.CurrentHealth -= actionManager.IncomingDamage;
            actionManager.IncomingDamage = 0;
        }

        //check if we killed a monster
        if (gameManager.TargetedMonster?.CurrentHealth <= 0)
            actionManager.killDuringAction = true;

        gameManager.TargetedLane = null;
        gameManager.TargetedHero = null;
        gameManager.TargetedMonster = null;
        gameManager.TargetedMonsterObject = null;

        actionManager.ActionCounter++;
        actionManager.ActionExecutor();
    }

    public static int CalculateAttack(int attackValueFromCard, MonsterModel monsterToAttack)
    {
        var gameManager = GameManager.Instance;
        var actionManager = ActionManager.Instance;

        //ATTACK FROM CARD
        var attackValue = attackValueFromCard;

        //ADD ALTERED VALUE
        attackValue += actionManager.AlterNextValue;
        actionManager.AlterNextValue = 0;

        //MARKED
        attackValue += monsterToAttack.Marked;

        //ARMOURED
        if (monsterToAttack.BaseMonster.MonsterAttributes.Contains(MonsterAttributeEnum.Armoured))
        attackValue -= 2;

        //REMOVE INITIATIVE
        gameManager.ActiveHero.Initiative = false;

        return attackValue;
    }

    public static void Jump() 
    {
        var gameManager = GameManager.Instance;

        if (gameManager.TargetedLane.HeroesModels.Count() < 3)
        {
            var heroLane = gameManager.HeroLanes.First(x => x.IsHeroHere(gameManager.ActiveHero));

            GameObject heroGameObject = null;
            foreach (var go in heroLane.ObjectsInLane)
            {
                var comp = go.GetComponent<HeroScript>();
                if (comp.HeroModel == gameManager.ActiveHero)
                {
                    heroGameObject = go;
                    break;
                }
            }

            gameManager.AddHeroToLane(heroGameObject, gameManager.ActiveHero, gameManager.TargetedLane.laneNumber);
            gameManager.ActiveHero.Initiative = true;
        }
    }
}
