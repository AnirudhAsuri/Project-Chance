using UnityEngine;

public class BombHandler : MonoBehaviour
{
    [SerializeField] private float fieldOfImpactRadius;
    [SerializeField] private float explosionForce;
    [SerializeField] private float explosionMultiplier;
    [SerializeField] private LayerMask layerToHit;

    public void HandleBombBlowing()
    {
        Debug.Log("Bomb Blowing");

        Collider2D[] affectedObjects = Physics2D.OverlapCircleAll(transform.position, fieldOfImpactRadius, layerToHit);

        foreach(Collider2D collider in affectedObjects)
        {
            float distanceToObject = (collider.transform.position - transform.position).magnitude;
            Vector2 explosionDirection = (collider.transform.position - transform.position).normalized;

            Vector2 finalExplosionForce = explosionForce * explosionMultiplier * explosionDirection;

            collider.attachedRigidbody.AddForce(finalExplosionForce, ForceMode2D.Force);
        }

        Destroy(gameObject);
    }

    // Place this entire method inside your BombHandler class

    private void OnDrawGizmosSelected()
    {
        // Set the color for the gizmo
        Gizmos.color = Color.red;

        // Draw a wireframe sphere (which looks like a circle in 2D)
        // It uses the bomb's position as the center and your fieldOfImpactRadius for the radius
        Gizmos.DrawWireSphere(transform.position, fieldOfImpactRadius);
    }
}
