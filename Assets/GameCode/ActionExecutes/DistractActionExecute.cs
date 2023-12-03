public class DistractActionExecute : IActionExecute
{
    private GameManager gameManager;
    private ActionManager actionManager;

    public DistractActionExecute()
    {
        gameManager = GameManager.Instance;
        actionManager = ActionManager.Instance;
    }

    public void Execute()
    {
        gameManager.TargetedMonster.Distract += actionManager.ActiveAction.Value;

        ActionExecuteHelper.EndOfExecute();
    }
}
