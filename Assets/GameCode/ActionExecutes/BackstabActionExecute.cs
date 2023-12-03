public class BackstabActionExecute : IActionExecute
{
    private GameManager gameManager;
    private ActionManager actionManager;

    public BackstabActionExecute()
    {
        gameManager = GameManager.Instance;
        actionManager = ActionManager.Instance;
    }

    public void Execute()
    {
        var totalDamage = 0;

        if (gameManager.ActiveHero.Initiative)
            totalDamage += 3;

        totalDamage += ActionExecuteHelper.CalculateAttack(actionManager.ActiveAction.Value);

        gameManager.TargetedMonster.CurrentHealth -= totalDamage;

        ActionExecuteHelper.EndOfExecute();
    }
}
