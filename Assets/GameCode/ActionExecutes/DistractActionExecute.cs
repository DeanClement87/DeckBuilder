using System.Linq;
using System.Threading;

public class DistractActionExecute : IActionExecute
{
    private GameManager gameManager;
    private ActionManager actionManager;

    public DistractActionExecute()
    {
        gameManager = GameManager.Instance;
        actionManager = ActionManager.Instance;
    }

    public void Execute()
    {
        //DISTRACT ACTIVE HERO LANE
        if (actionManager.ActiveAction.Target == ActionTargetEnum.MonsterLane)
        {
            var activeHeroLane = gameManager.HeroLanes.First(x => x.IsHeroHere(gameManager.ActiveHero));

            foreach (var monster in activeHeroLane.OppositeLane.MonsterModels)
            {
                monster.Distract += actionManager.ActiveAction.Value;
            }

            foreach (var monsterObject in activeHeroLane.OppositeLane.ObjectsInLane)
            {
                ParticleHelper.PerformParticleSequence(actionManager.ActiveAction.Particle,
                    actionManager.ActiveAction.ParticleBehavour,
                    monsterObject);
            }
        }
        else //DISTRACT SINGLE TARGET
        {
            gameManager.TargetedMonster.Distract += actionManager.ActiveAction.Value;

            ParticleHelper.PerformParticleSequence(actionManager.ActiveAction.Particle,
                actionManager.ActiveAction.ParticleBehavour,
                gameManager.TargetedMonsterObject);
        }

        ActionExecuteHelper.EndOfExecute();
    }
}
