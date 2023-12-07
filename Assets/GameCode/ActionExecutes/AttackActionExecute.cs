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

        //sometimes we want to delay the end of action, for example we want to wait until the
        //projectice particle hits the target to end the action and do the next one
        var delayEnd = false;
        GameObject particleObject = new GameObject();
        switch (actionManager.ActiveAction.Particle)
        {
            case ParticleEnum.Slash:
                ParticleHelper.Create(ParticleEnum.Slash, gameManager.TargetedMonsterObject);
                break;
            case ParticleEnum.Fireball:
                particleObject = ParticleHelper.Create(ParticleEnum.Fireball, gameManager.ActiveHeroObject);
                break;
        }

        switch (actionManager.ActiveAction.ParticleBehavour)
        {
            case ParticleBehavourEnum.Projectile:
                var projectileScript = particleObject.AddComponent<ProjectileScript>();
                projectileScript.Go(gameManager.TargetedMonsterObject, ParticleEnum.Explosion);
                delayEnd = true;
                break;
        }

        if (delayEnd == false)
            ActionExecuteHelper.EndOfExecute();
    }
}
