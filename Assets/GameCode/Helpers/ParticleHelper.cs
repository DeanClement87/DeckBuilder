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
        }

        particleObject.transform.SetParent(gameManager.MainCanvas.transform, true);

        return particleObject;
    }

}
