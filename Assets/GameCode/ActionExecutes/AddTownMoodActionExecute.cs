public class AddTownMoodActionExecute : IActionExecute
{
    private GameManager gameManager;
    private ActionManager actionManager;

    public AddTownMoodActionExecute()
    {
        gameManager = GameManager.Instance;
        actionManager = ActionManager.Instance;
    }

    public void Execute()
    {
        gameManager.Town.Mood += actionManager.ActiveAction.Value;

        if (gameManager.Town.Mood < -5)     gameManager.Town.Mood = -5;
        else if (gameManager.Town.Mood > 5) gameManager.Town.Mood = 5;

        ActionExecuteHelper.EndOfExecute();
    }
}
