public class MarkActionExecute : IActionExecute
{
    private GameManager gameManager;
    private ActionManager actionManager;

    public MarkActionExecute()
    {
        gameManager = GameManager.Instance;
        actionManager = ActionManager.Instance;
    }

    public void Execute()
    {
        gameManager.TargetedMonster.Marked += actionManager.ActiveAction.Value;

        ActionExecuteHelper.EndOfExecute();
    }
}
