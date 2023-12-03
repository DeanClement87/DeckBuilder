using System.Linq;

public class FriendlyInLaneCheck : ICardConditionCheck
{
    private GameManager gameManager;

    public FriendlyInLaneCheck()
    {
        gameManager = GameManager.Instance;
    }

    public bool ConditionMet()
    {
        var activeHeroLane = gameManager.HeroLanes.First(x => x.IsHeroHere(gameManager.ActiveHero));

        if (activeHeroLane.HeroesModels.Count() > 1) return true;
        else return false;
    }
}
