using UnityEngine;

public class BombHandler : MonoBehaviour
{
    [SerializeField] private float fieldOfImpactRadius;
    [SerializeField] private float explosionForce;
    [SerializeField] private LayerMask layerToHit;

    private void FixedUpdate()
    {
        
    }

    public void HandleBombBlowing()
    {
        Collider2D[] affectedObjects = Physics2D.OverlapCircleAll(transform.position, fieldOfImpactRadius, layerToHit);

        foreach(Collider2D collider in affectedObjects)
        {
            float distanceToObject = (transform.position - collider.transform.position).magnitude;
            Vector2 explosionDirection = (transform.position - collider.transform.position).normalized;

            Vector2 finalExplosionForce = explosionForce * distanceToObject / 10 * explosionDirection;

            collider.attachedRigidbody.AddForce(finalExplosionForce, ForceMode2D.Force);
        }
    }
}
