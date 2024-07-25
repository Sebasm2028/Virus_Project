using UnityEngine;
using UnityEngine.UI;

public class ZombieNest : MonoBehaviour
{
    public int maxHealth = 10;
    private int currentHealth;
    public Image healthImage; // UI Slider para mostrar la vida

    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            DestroyNest();
        }
        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        if (healthImage != null)
        {
            healthImage.fillAmount = (float)currentHealth / maxHealth;
        }
    }

    private void DestroyNest()
    {
        Debug.Log("Nest destroyed! You win!");
        // Lógica adicional como animación de destrucción, mensaje de victoria, etc.
    }
}
