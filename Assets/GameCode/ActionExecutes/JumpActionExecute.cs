using System.Linq;
using UnityEngine;

public class JumpActionExecute : IActionExecute
{
    private GameManager gameManager;

    public JumpActionExecute()
    {
        gameManager = GameManager.Instance;
    }

    public void Execute()
    {
        ActionExecuteHelper.Jump();

        ActionExecuteHelper.EndOfExecute();
    }
}
