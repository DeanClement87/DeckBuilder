using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ActionManager : MonoBehaviour
{
    private static ActionManager _instance;

    public static ActionManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<ActionManager>();

                if (_instance == null)
                {
                    GameObject singletonObject = new GameObject("ActionManager");
                    _instance = singletonObject.AddComponent<ActionManager>();

                }
            }

            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public ActionModel ActiveAction { get; set; }

    public int IncomingDamage { get; set; } = 0;
    public int ActionCounter { get; set; }
    public bool killDuringAction = false;
    public int AlterNextValue = 0;

    public void ActionExecutor()
    {
        var gameManager = GameManager.Instance;

        //last action just happened, so clean up.
        if (ActionCounter >= gameManager.ActiveCard.BaseCard.Actions.Count())
        {
            gameManager.gameState = GameManager.GameState.HeroTurn;
            ActiveAction = null;
            killDuringAction = false;
            AlterNextValue = 0;
            return;
        }

        ActiveAction = gameManager.ActiveCard.BaseCard.Actions[ActionCounter];

        if (ActiveAction.Target == ActionTargetEnum.Auto)
        {
            var actionExecute = ActionExecuteFactory.GetActionExecute(ActiveAction.Action);
            actionExecute.Execute();
        }

        if (ActiveAction.Target == ActionTargetEnum.AutoSkipAction)
        {
            var actionExecute = ActionExecuteFactory.GetActionExecute(ActiveAction.Action);
            actionExecute.Execute();
        }
    }
}
