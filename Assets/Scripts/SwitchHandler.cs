using UnityEngine;
using UnityEngine.Events;

public class SwitchHandler : MonoBehaviour
{
    private ProbabilityHandler probabilityHandler;

    [SerializeField] private string playerTag = "Player";
    public UnityEvent switchTriggerEvent;

    private bool hasTriggered = false;

    private void Start()
    {
        probabilityHandler = GetComponent<ProbabilityHandler>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(playerTag) && !hasTriggered)
        {
            hasTriggered = true;
            HandleSwitchTrigger();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(playerTag))
        {
            hasTriggered = false;
        }
    }

    public void HandleSwitchTrigger()
    {
        switchTriggerEvent.Invoke();
    }
}
