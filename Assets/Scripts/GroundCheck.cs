using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] private float size;
    [SerializeField] private LayerMask groundLayer;

    [SerializeField] private float bufferDistance;

    private void Start()
    {
        if(GetComponent<BoxCollider2D>() != null)
        {
            size = GetComponent<BoxCollider2D>().size.y;
        }
        else if(GetComponent<CapsuleCollider2D>() != null)
        {
            size = GetComponent<CapsuleCollider2D>().size.y;
        }
    }

    public bool HandleGroundCheck()
    {
        Debug.DrawRay(transform.position, -Vector2.up * (size / 2 + bufferDistance), Color.red);

        if (Physics2D.Raycast(transform.position, -Vector2.up, size / 2 + bufferDistance, groundLayer))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}