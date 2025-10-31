using UnityEngine;

public class BombHandler : MonoBehaviour
{
    [SerializeField] private float fieldOfImpactRadius;
    [SerializeField] private float explosionForce;
    [SerializeField] private float explosionMultiplier;
    [SerializeField] private LayerMask layerToHit;

    [SerializeField] private int maxDamage;
    [SerializeField] private int medDamage;
    [SerializeField] private int minDamage;
    private int damageDealt;

    private string playerTag = "Player";

    [SerializeField] private bool isMultipleUse;

    public void HandleBombBlowing()
    {
        float innerRadius = fieldOfImpactRadius * 0.33f;
        float midRadius = fieldOfImpactRadius * 0.66f;
        float outerRadius = fieldOfImpactRadius;

        Collider2D[] affectedObjects = Physics2D.OverlapCircleAll(transform.position, fieldOfImpactRadius, layerToHit);

        foreach(Collider2D collider in affectedObjects)
        {
            float distanceToObject = (collider.transform.position - transform.position).magnitude;
            float forceDropoff;

            if(distanceToObject <= innerRadius)
            {
                forceDropoff = 1.0f;
                damageDealt = maxDamage;
            }
            else if(distanceToObject <= midRadius)
            {
                forceDropoff = 0.8f;
                damageDealt = medDamage;
            }
            else if(distanceToObject <= outerRadius)
            {
                forceDropoff = 0.6f;
                damageDealt = minDamage;
            }
            else
            {
                forceDropoff = 0f;
                damageDealt = 0;
            }

            Vector2 explosionDirection = (collider.transform.position - transform.position).normalized;
            Vector2 finalExplosionForce = explosionForce * explosionMultiplier * explosionDirection * forceDropoff;
            collider.attachedRigidbody.AddForce(finalExplosionForce, ForceMode2D.Force);

            if (collider.transform.CompareTag(playerTag))
            {
                collider.GetComponent<PlayerHealth>().TakeDamage(damageDealt);
            }
        }

        if(!isMultipleUse)
            Destroy(gameObject);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red; Gizmos.DrawWireSphere(transform.position, fieldOfImpactRadius * 0.33f);
        Gizmos.color = Color.yellow; Gizmos.DrawWireSphere(transform.position, fieldOfImpactRadius * 0.66f);
        Gizmos.color = Color.green; Gizmos.DrawWireSphere(transform.position, fieldOfImpactRadius);
    }
}
