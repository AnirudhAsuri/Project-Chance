using UnityEngine;
using UnityEngine.Events;

public class ProbabilityHandler : MonoBehaviour
{
    public UnityEvent triggerEvent;
    public UnityEvent resultEvent;

    [SerializeField, Range(0f, 1f)] private float probability;

    private void Start()
    {
        probability = Mathf.Clamp01(probability);
        triggerEvent.AddListener(HandleProbabilityEvent);
    }

    public void HandleProbabilityEvent()
    {
        Debug.Log("Triggered");

        float randNum = Random.value;

        Debug.Log(randNum);

        if(randNum <= probability)
        {
            Debug.Log("Success");
            resultEvent.Invoke();
        }
        else
        {
            Debug.Log("Fail");
        }
    }
}