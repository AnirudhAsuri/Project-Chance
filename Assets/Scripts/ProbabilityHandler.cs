using UnityEngine;
using UnityEngine.Events;

public class ProbabilityHandler : MonoBehaviour
{
    public UnityEvent resultEvent;

    [SerializeField] private float probability;

    private void Start()
    {
        probability = Mathf.Clamp01(probability);
    }

    public void HandleProbabilityEvent()
    { 

        float randNum = Random.value;

        if(randNum <= probability)
        {
            resultEvent.Invoke();
        }
    }
}