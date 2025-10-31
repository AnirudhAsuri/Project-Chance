using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int currentCertaintyCells;
    [SerializeField] private int totalCertaintyCells;

    [SerializeField] private float probabilityIncreaseRange;
    public LayerMask probabilityObjectLayer;

    public Image[] certaintyCells;

    public Sprite fullCell;
    public Sprite emptyCell;

    private void Update()
    {
        foreach (Image img in certaintyCells)
        {
            img.sprite = emptyCell;
        }
            
        for(int i = 0; i < currentCertaintyCells; i++)
        {
            certaintyCells[i].sprite = fullCell;
        }
    }

    public void TakeDamage(int damage)
    {
        currentCertaintyCells -= damage;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, probabilityIncreaseRange, probabilityObjectLayer);

        foreach (Collider2D collider in colliders)
        {
            ProbabilityHandler probabilityHandler = collider.GetComponent<ProbabilityHandler>();

            if (probabilityHandler != null)
            {
                probabilityHandler.IncreaseProbabilityLevel();
            }
            else
            {
                Debug.LogWarning("Found " + collider.name + " but it has no ProbabilityHandler script");
            }
        }
    }

    public void HealHealth(int healHealth)
    {
        currentCertaintyCells += healHealth;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green; Gizmos.DrawWireSphere(transform.position, probabilityIncreaseRange);
    }
}