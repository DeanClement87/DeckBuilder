using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    private GameObject target;
    private ParticleEnum impactParticle;
    public float speed = 1.5f;

    public void Go(GameObject target, ParticleEnum impactParticle)
    {
        this.target = target;
        this.impactParticle = impactParticle;
    }

    void Update()
    {
        if (target == null) return;

        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed);

        if (Vector2.Distance(transform.position, target.transform.position) < 0.1f)
        {
            ParticleHelper.Create(impactParticle, target);
            Destroy(gameObject);
            ActionExecuteHelper.EndOfExecute();
        }
    }
}
