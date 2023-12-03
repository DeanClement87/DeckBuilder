public class AttackActionExecute : IActionExecute
{
    private GameManager gameManager;
    private ActionManager actionManager;

    public AttackActionExecute()
    {
        gameManager = GameManager.Instance;
        actionManager = ActionManager.Instance;
    }

    public void Execute()
    {
        gameManager.TargetedMonster.CurrentHealth -= ActionExecuteHelper.CalculateAttack(actionManager.ActiveAction.Value);

        ActionExecuteHelper.EndOfExecute();
    }
}
