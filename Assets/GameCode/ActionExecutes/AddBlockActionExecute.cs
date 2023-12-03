public class AddBlockActionExecute : IActionExecute
{
    private GameManager gameManager;
    private ActionManager actionManager;

    public AddBlockActionExecute()
    {
        gameManager = GameManager.Instance;
        actionManager = ActionManager.Instance;
    }

    public void Execute()
    {
        gameManager.TargetedHero.Block += actionManager.ActiveAction.Value;

        ActionExecuteHelper.EndOfExecute();
    }
}
