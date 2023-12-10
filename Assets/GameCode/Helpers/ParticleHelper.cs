using UnityEngine;
public static class ParticleHelper
{
    public static GameObject Create(ParticleEnum particleEnum, GameObject target)
    {
        var gameManager = GameManager.Instance;

        GameObject particleObject = new GameObject();
        GameObject particlePrefab;

        switch (particleEnum)
        {
            case ParticleEnum.Slash:
                particlePrefab = Resources.Load<GameObject>("JMO Assets/Cartoon FX Remaster/CFXR Prefabs/Sword Trails/Fire/CFXR4 Sword Hit FIRE (Slash)");
                particleObject = GameObject.Instantiate(particlePrefab, target.transform.position, Quaternion.identity);
                particleObject.transform.localScale = new Vector3(150, 150, 0);
                break;
            case ParticleEnum.Fireball:
                particlePrefab = Resources.Load<GameObject>("JMO Assets/Cartoon FX Remaster/CFXR Prefabs/Fire/CFXR4 Sun");
                particleObject = GameObject.Instantiate(particlePrefab, target.transform.position, Quaternion.identity);
                particleObject.transform.localScale = new Vector3(80, 80, 0);
                break;
            case ParticleEnum.Explosion:
                particlePrefab = Resources.Load<GameObject>("JMO Assets/Cartoon FX Remaster/CFXR Prefabs/Explosions/CFXR4 Explosion Orange (HDR) + Smoke");
                particleObject = GameObject.Instantiate(particlePrefab, target.transform.position, Quaternion.identity);
                particleObject.transform.localScale = new Vector3(100, 100, 0);
                break;
            case ParticleEnum.Chaosbolt:
                particlePrefab = Resources.Load<GameObject>("JMO Assets/Cartoon FX Remaster/CFXR Prefabs/Space/CFXR4 Laser Donut + Trail (Green)");
                particleObject = GameObject.Instantiate(particlePrefab, target.transform.position, Quaternion.identity);
                particleObject.transform.localScale = new Vector3(150, 150, 0);
                break;
            case ParticleEnum.ChaosboltImpact:
                particlePrefab = Resources.Load<GameObject>("JMO Assets/Cartoon FX Remaster/CFXR Prefabs/Space/CFXR4 Laser Donut Impact (Green)");
                particleObject = GameObject.Instantiate(particlePrefab, target.transform.position, Quaternion.identity);
                particleObject.transform.localScale = new Vector3(150, 150, 0);
                break;
            case ParticleEnum.Backstab:
                particlePrefab = Resources.Load<GameObject>("JMO Assets/Cartoon FX Remaster/CFXR Prefabs/Space/CFXR4 Plasma Shoot (Blue)");
                particleObject = GameObject.Instantiate(particlePrefab, target.transform.position, Quaternion.identity);
                particleObject.transform.localScale = new Vector3(100, 100, 0);
                break;

        }

        particleObject.transform.SetParent(gameManager.MainCanvas.transform, true);

        return particleObject;
    }

    public static bool PerformParticleSequence(ParticleEnum particleEnum, ParticleBehavourEnum particleBehavourEnum)
    {
        var gameManager = GameManager.Instance;

        //sometimes we want to delay the end of action, for example we want to wait until the
        //projectice particle hits the target to end the action and do the next one
        bool delayEnd = false;

        GameObject particleObject = new GameObject();
        ParticleEnum impactParticle = ParticleEnum.None;
        switch (particleEnum)
        {
            case ParticleEnum.Slash:
                ParticleHelper.Create(ParticleEnum.Slash, gameManager.TargetedMonsterObject);
                break;
            case ParticleEnum.Backstab:
                ParticleHelper.Create(ParticleEnum.Backstab, gameManager.TargetedMonsterObject);
                break;
            case ParticleEnum.Fireball:
                particleObject = ParticleHelper.Create(ParticleEnum.Fireball, gameManager.ActiveHeroObject);
                impactParticle = ParticleEnum.Explosion;
                break;
            case ParticleEnum.Chaosbolt:
                particleObject = ParticleHelper.Create(ParticleEnum.Chaosbolt, gameManager.ActiveHeroObject);
                impactParticle = ParticleEnum.ChaosboltImpact;
                break;
        }

        switch (particleBehavourEnum)
        {
            case ParticleBehavourEnum.Projectile:
                var projectileScript = particleObject.AddComponent<ProjectileScript>();
                projectileScript.Go(gameManager.TargetedMonsterObject, impactParticle);
                delayEnd = true;
                break;
        }

        return delayEnd;
    }

}
