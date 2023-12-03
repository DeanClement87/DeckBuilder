using System.Linq;

public class MonsterInThisLaneCheck : ICardConditionCheck
{
    private GameManager gameManager;

    public MonsterInThisLaneCheck()
    {
        gameManager = GameManager.Instance;
    }

    public bool ConditionMet()
    {
        var activeHeroLane = gameManager.HeroLanes.First(x => x.IsHeroHere(gameManager.ActiveHero));

        if (activeHeroLane.OppositeLane.MonsterModels.Any()) return true;
        else return false;
    }
}
