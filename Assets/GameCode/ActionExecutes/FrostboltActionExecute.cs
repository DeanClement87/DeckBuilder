using UnityEngine;

public class FrostboltActionExecute : IActionExecute
{
    private GameManager gameManager;
    private ActionManager actionManager;

    public FrostboltActionExecute()
    {
        gameManager = GameManager.Instance;
        actionManager = ActionManager.Instance;
    }

    public void Execute()
    {
        var attackValue = actionManager.ActiveAction.Value;

        if (gameManager.TargetedMonster.Frozen)
            attackValue += 8;
        else
            gameManager.TargetedMonster.Frozen = true;

        actionManager.IncomingDamage = ActionExecuteHelper.CalculateAttack(attackValue, gameManager.TargetedMonster);


        var delayEnd = ParticleHelper.PerformParticleSequence(actionManager.ActiveAction.Particle, 
            actionManager.ActiveAction.ParticleBehavour, 
            gameManager.TargetedMonsterObject, 
            gameManager.ActiveHeroObject);

        if (delayEnd == false)
            ActionExecuteHelper.EndOfExecute();
    }
}
