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
        int laneNumber = 0;
        if (actionManager.ActiveAction.StringValue != "")
            laneNumber = int.Parse(actionManager.ActiveAction.StringValue);

        if (laneNumber == 0)
        {
            var activeHeroLane = gameManager.HeroLanes.First(x => x.IsHeroHere(gameManager.ActiveHero));
            laneNumber = activeHeroLane.laneNumber;
        }

        //if there are no monsters in lane then activate a skip
        if (gameManager.MonsterLanes[laneNumber-1].MonsterModels.Any(x => x.CurrentHealth > 0) == false)
            actionManager.ActionCounter += actionManager.ActiveAction.Value;          

        ActionExecuteHelper.EndOfExecute();
    }
}
