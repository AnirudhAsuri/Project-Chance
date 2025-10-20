using UnityEngine;
using UnityEngine.Events;

public class SwitchHandler : MonoBehaviour
{
    [SerializeField] private string playerTag = "Player";
    public UnityEvent switchTriggerEvent;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.CompareTag(playerTag))
        {
            HandleSwitchTrigger();
        }
    }

    public void HandleSwitchTrigger()
    {
        switchTriggerEvent.Invoke();
    }
}
