using UnityEngine;

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
        actionManager.IncomingDamage = ActionExecuteHelper.CalculateAttack(actionManager.ActiveAction.Value);

        var delayEnd = ParticleHelper.PerformParticleSequence(actionManager.ActiveAction.Particle, actionManager.ActiveAction.ParticleBehavour);

        if (delayEnd == false)
            ActionExecuteHelper.EndOfExecute();
    }
}
