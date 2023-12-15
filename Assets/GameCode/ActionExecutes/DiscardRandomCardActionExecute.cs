public class DiscardRandomCardActionExecute : IActionExecute
{
    private GameManager gameManager;
    private ActionManager actionManager;

    public DiscardRandomCardActionExecute()
    {
        gameManager = GameManager.Instance;
        actionManager = ActionManager.Instance;
    }

    public void Execute()
    {
        for (int i = 0; i < actionManager.ActiveAction.Value; i++)
            gameManager.ActiveHero.DiscardRandomCard();

        ActionExecuteHelper.EndOfExecute();
    }
}
