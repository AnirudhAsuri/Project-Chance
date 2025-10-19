using UnityEngine;

public class SwitchHandler : MonoBehaviour
{
    [SerializeField] private string playerTag = "Player";

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.CompareTag(playerTag))
        {
            HandleSwitchTrigger();
        }
    }

    public void HandleSwitchTrigger()
    {
        Debug.Log("Trigger Called");
    }
}
