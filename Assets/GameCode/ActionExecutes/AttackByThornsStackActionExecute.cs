public class AttackByThornsStackActionExecute : IActionExecute
{
    private GameManager gameManager;

    public AttackByThornsStackActionExecute()
    {
        gameManager = GameManager.Instance;
    }

    public void Execute()
    {
        gameManager.TargetedMonster.CurrentHealth -= ActionExecuteHelper.CalculateAttack(gameManager.ActiveHero.Thorns);

        ActionExecuteHelper.EndOfExecute();
    }
}
