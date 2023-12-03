public class AddThornsActionExecute : IActionExecute
{
    private GameManager gameManager;
    private ActionManager actionManager;

    public AddThornsActionExecute()
    {
        gameManager = GameManager.Instance;
        actionManager = ActionManager.Instance;
    }

    public void Execute()
    {
        gameManager.TargetedHero.Thorns += actionManager.ActiveAction.Value;

        ActionExecuteHelper.EndOfExecute();
    }
}
