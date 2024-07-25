using UnityEngine;
using UnityEngine.UI;

public class ZombieNest : MonoBehaviour
{
    public float maxHealth = 10;
    private float currentHealth;
    public Image healthImage; // UI Slider para mostrar la vida
    public bool isDead = false;

    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0 && !isDead)
        {
            isDead = true;
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
}
