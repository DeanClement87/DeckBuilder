public class AttackPlusFearActionExecute : IActionExecute
{
    private GameManager gameManager;
    private ActionManager actionManager;

    public AttackPlusFearActionExecute()
    {
        gameManager = GameManager.Instance;
        actionManager = ActionManager.Instance;
    }

    public void Execute()
    {
        gameManager.TargetedMonster.CurrentHealth -= ActionExecuteHelper.CalculateAttack(actionManager.ActiveAction.Value) + gameManager.Town.Fear();

        ActionExecuteHelper.EndOfExecute();
    }
}
