using UnityEngine;

public class GiveManaToAlliesActionExecute : IActionExecute
{
    private GameManager gameManager;
    private ActionManager actionManager;
    private string condition;

    public GiveManaToAlliesActionExecute(string condition = null)
    {
        gameManager = GameManager.Instance;
        actionManager = ActionManager.Instance;
    }

    public void Execute()
    {
        foreach (var hero in gameManager.TargetedLane.HeroesModels)
        {
            if (condition == "not_to_self")
            {
                if (hero == gameManager.ActiveHero) continue;
            }

            hero.Mana += actionManager.ActiveAction.Value;
            if (hero.Mana > 5) hero.Mana = 5;
        }

        ActionExecuteHelper.EndOfExecute();
    }
}
