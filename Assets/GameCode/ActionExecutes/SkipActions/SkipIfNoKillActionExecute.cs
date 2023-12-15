using System.Linq;

public class SkipIfNoKillActionExecute : IActionExecute
{
    private GameManager gameManager;
    private ActionManager actionManager;

    public SkipIfNoKillActionExecute()
    {
        gameManager = GameManager.Instance;
        actionManager = ActionManager.Instance;
    }

    public void Execute()
    {
        if (actionManager.killDuringAction == false)
            actionManager.ActionCounter += actionManager.ActiveAction.Value;

        ActionExecuteHelper.EndOfExecute();
    }
}
