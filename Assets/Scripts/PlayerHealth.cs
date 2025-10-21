using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int currentCertaintyCells;
    [SerializeField] private int totalCertaintyCells;

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
    }
}