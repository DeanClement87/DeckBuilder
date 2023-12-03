using System.Collections.Generic;
using System.Linq;

public class MonsterOutsideLaneCheck : ICardConditionCheck
{
    private GameManager gameManager;

    public MonsterOutsideLaneCheck()
    {
        gameManager = GameManager.Instance;
    }

    public bool ConditionMet()
    {
        var outsideLanes = gameManager.HeroLanes.Where(x => x.IsHeroHere(gameManager.ActiveHero) == false);

        foreach (var outsideLane in outsideLanes)
        {
            if (outsideLane.OppositeLane.MonsterModels.Any()) return true;
        }

        return false;
    }
}
