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
            case ParticleEnum.IceSpin:
                particlePrefab = Resources.Load<GameObject>("JMO Assets/Cartoon FX Remaster/CFXR Prefabs/Sword Trails/Ice/CFXR4 Sword Trail ICE (360 Thick)");
                particleObject = GameObject.Instantiate(particlePrefab, target.transform.position, Quaternion.identity);
                particleObject.transform.localScale = new Vector3(150, 150, 0);
                break;
            case ParticleEnum.Nightmare:
                particlePrefab = Resources.Load<GameObject>("JMO Assets/Cartoon FX Remaster/CFXR Prefabs/Explosions/CFXR4 Wave Explosion Purple");
                particleObject = GameObject.Instantiate(particlePrefab, target.transform.position, Quaternion.identity);
                particleObject.transform.localScale = new Vector3(150, 150, 0);
                break;
            case ParticleEnum.Arrow:
                particlePrefab = Resources.Load<GameObject>("JMO Assets/Cartoon FX Remaster/CFXR Prefabs/Space/CFXR4 Laser + Trail (Orange)");
                particleObject = GameObject.Instantiate(particlePrefab, target.transform.position, Quaternion.identity);
                particleObject.transform.localScale = new Vector3(150, 150, 0);
                break;
            case ParticleEnum.ArrowImpact:
                particlePrefab = Resources.Load<GameObject>("JMO Assets/Cartoon FX Remaster/CFXR Prefabs/Space/CFXR4 Laser Impact (Orange)");
                particleObject = GameObject.Instantiate(particlePrefab, target.transform.position, Quaternion.identity);
                particleObject.transform.localScale = new Vector3(150, 150, 0);
                break;

        }

        particleObject.transform.SetParent(gameManager.MainCanvas.transform, true);

        return particleObject;
    }

    public static bool PerformParticleSequence(ParticleEnum particleEnum, ParticleBehavourEnum particleBehavourEnum, GameObject target, GameObject start = null)
    {
        var gameManager = GameManager.Instance;

        //sometimes we want to delay the end of action, for example we want to wait until the
        //projectice particle hits the target to end the action and do the next one
        bool delayEnd = false;

        GameObject particleObject = new GameObject();
        ParticleEnum impactParticle = ParticleEnum.None;
        float projectileSpeed = 0;
        switch (particleEnum)
        {
            case ParticleEnum.Slash:
                Create(ParticleEnum.Slash, target);
                break;
            case ParticleEnum.IceSpin:
                Create(ParticleEnum.IceSpin, target);
                break;
            case ParticleEnum.Backstab:
                Create(ParticleEnum.Backstab, target);
                break;
            case ParticleEnum.Nightmare:
                Create(ParticleEnum.Nightmare, target);
                break;
            case ParticleEnum.Explosion:
                Create(ParticleEnum.Explosion, target);
                break;
            case ParticleEnum.Arrow:
                particleObject = Create(ParticleEnum.Arrow, start);
                impactParticle = ParticleEnum.ArrowImpact;
                projectileSpeed = 20.0f;
                break;
            case ParticleEnum.Fireball:
                particleObject = Create(ParticleEnum.Fireball, start);
                impactParticle = ParticleEnum.Explosion;
                projectileSpeed = 3.0f;
                break;
            case ParticleEnum.Chaosbolt:
                particleObject = Create(ParticleEnum.Chaosbolt, start);
                impactParticle = ParticleEnum.ChaosboltImpact;
                projectileSpeed = 8.0f;
                break;
        }

        switch (particleBehavourEnum)
        {
            case ParticleBehavourEnum.Projectile:
                var projectileScript = particleObject.AddComponent<ProjectileScript>();
                projectileScript.Go(target, impactParticle, projectileSpeed);
                delayEnd = true;
                break;
        }

        return delayEnd;
    }

}
