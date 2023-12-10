using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    private GameObject target;
    private ParticleEnum impactParticle;
    private float speed;

    public void Go(GameObject target, ParticleEnum impactParticle, float speed)
    {
        this.target = target;
        this.impactParticle = impactParticle;
        this.speed = speed;
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
