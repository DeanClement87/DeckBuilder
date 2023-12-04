public class StunActionExecute : IActionExecute
{
    private GameManager gameManager;
    private ActionManager actionManager;

    public StunActionExecute()
    {
        gameManager = GameManager.Instance;
        actionManager = ActionManager.Instance;
    }

    public void Execute()
    {
        gameManager.TargetedMonster.Stunned = true;

        ActionExecuteHelper.EndOfExecute();
    }
}
