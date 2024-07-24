using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public Image healthImage; // La imagen de la barra de salud
    public Image damageImage; // La imagen de la barra de da�o
    public float maxHealth = 100f; // La salud m�xima del jugador
    private float currentHealth; // La salud actual del jugador
    private float damageCooldown = 3f; // Tiempo de espera para regeneraci�n
    private float damageRegeneration = 4f; // Cantidad de salud regenerada cada cooldown

    private void Start()
    {
        currentHealth = maxHealth; // Inicializa la salud actual al m�ximo
        UpdateHealthUI();
        InvokeRepeating("RegenerateHealth", damageCooldown, damageCooldown); // Repetir regeneraci�n cada cooldown
    }

    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount; // Reduce la salud actual
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Asegura que la salud no baje de 0 ni suba del m�ximo
        UpdateHealthUI();
    }

    private void RegenerateHealth()
    {
        if (currentHealth < maxHealth)
        {
            currentHealth += damageRegeneration; // Regenera salud
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Asegura que la salud no suba del m�ximo
            UpdateHealthUI();
        }
    }

    private void UpdateHealthUI()
    {
        float healthFillAmount = currentHealth / maxHealth;
        healthImage.rectTransform.localScale = new Vector3(healthFillAmount, 1, 1); // Actualiza la imagen de la barra de salud
        damageImage.fillAmount = 1f - healthFillAmount; // Actualiza la imagen de la barra de da�o
    }
}