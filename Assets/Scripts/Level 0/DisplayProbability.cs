using UnityEngine;
using TMPro;
public class DisplayProbability : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI probabilityText;
    private ProbabilityHandler probabilityHandler;

    [SerializeField] private float currentProbabiltyValue;
    [SerializeField] private float lastKnownProbabilityValue = -1f;

    private void Awake()
    {
        probabilityHandler = GetComponent<ProbabilityHandler>();
    }

    private void Start()
    {
        currentProbabiltyValue = probabilityHandler.probability;
    }

    private void Update()
    {
        currentProbabiltyValue = probabilityHandler.probability;

        if (currentProbabiltyValue != lastKnownProbabilityValue)
        {
            HandleProbabilityText();
        }
    }

    public void HandleProbabilityText()
    {
        probabilityText.text = (currentProbabiltyValue * 100).ToString("F0") + "%";

        lastKnownProbabilityValue = currentProbabiltyValue;
    }
}