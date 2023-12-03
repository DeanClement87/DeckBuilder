using System.Linq;

public class SkipIfNoMonsterInLaneActionExecute : IActionExecute
{
    private GameManager gameManager;
    private ActionManager actionManager;

    public SkipIfNoMonsterInLaneActionExecute()
    {
        gameManager = GameManager.Instance;
        actionManager = ActionManager.Instance;
    }

    public void Execute()
    {
        var activeHeroLane = gameManager.HeroLanes.First(x => x.IsHeroHere(gameManager.ActiveHero));

        //if there are no monsters in the monster lane, then confirm a skip
        if (activeHeroLane.OppositeLane.MonsterModels.Any(x => x.CurrentHealth > 0) == false)
            actionManager.ActionCounter += actionManager.ActiveAction.Value;

        ActionExecuteHelper.EndOfExecute();
    }
}
