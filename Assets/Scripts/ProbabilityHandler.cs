using UnityEngine;
using UnityEngine.Events;

public class ProbabilityHandler : MonoBehaviour
{
    public UnityEvent resultEvent;

    private float probability;

    public enum probabilityLevel
    {
        Red90, // 90%
        Orange75, // 75%
        Yellow50, // 50%
        Green25, // 25%
        Blue0, // 0%
        Entropy, // ENTROPY
    }

    [SerializeField] private probabilityLevel startLevel;
    [SerializeField] private probabilityLevel currentLevel;

    private void Start()
    {
        SetProbabilityLevel(startLevel);
    }

    public void HandleProbabilityEvent()
    {
        float randNum = Random.value;

        if(randNum <= probability)
        {
            resultEvent.Invoke();
        }
    }

    public void SetProbabilityLevel(probabilityLevel newLevel)
    {
        currentLevel = newLevel;
        UpdateProbabilityLevel();
    }

    public void DecreaseProbabilityLevel()
    {
        switch (currentLevel)
        {
            case probabilityLevel.Red90:
                SetProbabilityLevel(probabilityLevel.Orange75);
                break;

            case probabilityLevel.Orange75:
                SetProbabilityLevel(probabilityLevel.Yellow50);
                break;

            case probabilityLevel.Yellow50:
                SetProbabilityLevel(probabilityLevel.Green25);
                break;

            case probabilityLevel.Green25:
                SetProbabilityLevel(probabilityLevel.Blue0);
                break;

            case probabilityLevel.Blue0:
                break;
        }

        UpdateProbabilityLevel();
    }

    public void IncreaseProbabilityLevel()
    {
        switch (currentLevel)
        {
            case probabilityLevel.Blue0:
                SetProbabilityLevel(probabilityLevel.Green25);
                break;

            case probabilityLevel.Green25:
                SetProbabilityLevel(probabilityLevel.Yellow50);
                break;

            case probabilityLevel.Yellow50:
                SetProbabilityLevel(probabilityLevel.Orange75);
                break;

            case probabilityLevel.Orange75:
                SetProbabilityLevel(probabilityLevel.Red90);
                break;

            case probabilityLevel.Red90:
                break;
        }

        UpdateProbabilityLevel();
    }

    public void UpdateProbabilityLevel()
    {
        switch(currentLevel)
        {
            case probabilityLevel.Red90:
                probability = 0.90f;
                break;

            case probabilityLevel.Orange75:
                probability = 0.75f;
                break;

            case probabilityLevel.Yellow50:
                probability = 0.50f;
                break;

            case probabilityLevel.Green25:
                probability = 0.25f;
                break;

            case probabilityLevel.Blue0:
                probability = 0.0f;
                break;
        }
    }
}