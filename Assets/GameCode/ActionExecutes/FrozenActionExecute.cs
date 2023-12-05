public class FrozenActionExecute : IActionExecute
{
    private GameManager gameManager;
    private ActionManager actionManager;

    public FrozenActionExecute()
    {
        gameManager = GameManager.Instance;
        actionManager = ActionManager.Instance;
    }

    public void Execute()
    {
        gameManager.TargetedMonster.Frozen = true;

        ActionExecuteHelper.EndOfExecute();
    }
}