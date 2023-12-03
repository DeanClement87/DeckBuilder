public class GiveCardDrawToAlliesActionExecute : IActionExecute
{
    private GameManager gameManager;
    private ActionManager actionManager;

    public GiveCardDrawToAlliesActionExecute()
    {
        gameManager = GameManager.Instance;
        actionManager = ActionManager.Instance;
    }

    public void Execute()
    {
        foreach (var hero in gameManager.TargetedLane.HeroesModels)
        {
            if (hero == gameManager.ActiveHero) continue;

            hero.DrawCard(actionManager.ActiveAction.Value);
        }

        ActionExecuteHelper.EndOfExecute();
    }
}
