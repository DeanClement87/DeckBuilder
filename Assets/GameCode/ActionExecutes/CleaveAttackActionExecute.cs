using System.Linq;

public class CleaveAttackActionExecute : IActionExecute
{
    private GameManager gameManager;
    private ActionManager actionManager;
    private string condition;

    public CleaveAttackActionExecute(string condition = null)
    {
        gameManager = GameManager.Instance;
        actionManager = ActionManager.Instance;
        this.condition = condition; 
    }

    public void Execute()
    {
        var activeLane = gameManager.HeroLanes.First(x => x.IsHeroHere(gameManager.ActiveHero));

        foreach (var monster in activeLane.OppositeLane.MonsterModels.Where(x => x.CurrentHealth > 0))
        {
            //Check injured condition
            if (condition == "injured" && monster.CurrentHealth == monster.BaseMonster.Health) continue;

            monster.CurrentHealth -= ActionExecuteHelper.CalculateAttack(actionManager.ActiveAction.Value, monster);
        }

        ActionExecuteHelper.EndOfExecute();
    }
}
