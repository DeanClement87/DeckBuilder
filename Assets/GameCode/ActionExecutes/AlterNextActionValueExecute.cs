using System.Linq;

public class AlterNextActionValueExecute : IActionExecute
{
    private GameManager gameManager;
    private ActionManager actionManager;

    public AlterNextActionValueExecute()
    {
        gameManager = GameManager.Instance;
        actionManager = ActionManager.Instance;
    }

    public void Execute()
    {
        switch(actionManager.ActiveAction.StringValue)
        {                      
            case "addFear":
                actionManager.AlterNextValue = gameManager.Town.Fear();
                break;
            case "addThorns":
                actionManager.AlterNextValue = gameManager.ActiveHero.Thorns;
                break;
            case "backstab":
                if (gameManager.ActiveHero.Initiative)
                    actionManager.AlterNextValue = actionManager.ActiveAction.Value;
                break;
        }


        actionManager.ActionCounter++;
        actionManager.ActionExecutor();
    }
}
