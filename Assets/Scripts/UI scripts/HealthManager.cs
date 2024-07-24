using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public Image healthImage; // La imagen de la barra de salud (Filled)
    public Image damageImage; // La imagen de la barra de daño (Filled)
    public float maxHealth = 100f; // La salud máxima del jugador
    private float currentHealth; // La salud actual del jugador
    public float damagePerHit = 5f; // Daño por golpe del zombie
    public float healPerSecond = 2f; // Sanación por segundo

    private void Start()
    {
        currentHealth = maxHealth; // Inicializa la salud actual al máximo
        UpdateHealthUI();
    }

    private void Update()
    {
        // Lógica de sanación gradual
        if (currentHealth < maxHealth)
        {
            currentHealth += healPerSecond * Time.deltaTime;
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
            UpdateHealthUI();
        }
    }

    private void UpdateHealthUI()
    {
        float healthFillAmount = currentHealth / maxHealth;
        healthImage.fillAmount = healthFillAmount; // Actualiza la imagen de la barra de salud
        damageImage.fillAmount = 1f - healthFillAmount; // Actualiza la imagen de la barra de daño
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount; // Reduce la salud actual
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Asegura que la salud no baje de 0 ni suba del máximo
        UpdateHealthUI();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter called with: " + other.gameObject.name); // Mensaje de depuración

        if (other.CompareTag("Zombie"))
        {
            Debug.Log("Player collided with Zombie"); // Mensaje de depuración
            TakeDamage(damagePerHit);
        }
    }
}