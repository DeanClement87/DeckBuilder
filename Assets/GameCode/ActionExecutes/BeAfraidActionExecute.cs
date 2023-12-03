public class BeAfraidActionExecute : IActionExecute
{
    private GameManager gameManager;

    public BeAfraidActionExecute()
    {
        gameManager = GameManager.Instance;
    }

    public void Execute()
    {
        gameManager.ActiveHero.BeAfraid = true;

        ActionExecuteHelper.EndOfExecute();
    }
}
