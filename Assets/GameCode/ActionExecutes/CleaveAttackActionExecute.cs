using System.Linq;

public class CleaveAttackActionExecute : IActionExecute
{
    private GameManager gameManager;
    private ActionManager actionManager;

    public CleaveAttackActionExecute()
    {
        gameManager = GameManager.Instance;
        actionManager = ActionManager.Instance;
    }

    public void Execute()
    {
        var activeLane = gameManager.HeroLanes.First(x => x.IsHeroHere(gameManager.ActiveHero));

        foreach (var monster in activeLane.OppositeLane.MonsterModels)
        {
            monster.CurrentHealth -= ActionExecuteHelper.CalculateAttack(actionManager.ActiveAction.Value);
        }

        ActionExecuteHelper.EndOfExecute();
    }
}
