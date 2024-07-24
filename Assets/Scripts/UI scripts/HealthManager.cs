using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public Image healthImage; // La imagen de la barra de salud (Filled)
    public Image damageImage; // La imagen de la barra de da�o (Filled)
    public float maxHealth = 100f; // La salud m�xima del jugador
    private float currentHealth; // La salud actual del jugador
    public float damagePerHit = 5f; // Da�o por golpe del zombie
    public float healPerSecond = 2f; // Sanaci�n por segundo

    private void Start()
    {
        currentHealth = maxHealth; // Inicializa la salud actual al m�ximo
        UpdateHealthUI();
    }

    private void Update()
    {
        // L�gica de sanaci�n gradual
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
        damageImage.fillAmount = 1f - healthFillAmount; // Actualiza la imagen de la barra de da�o
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount; // Reduce la salud actual
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // Asegura que la salud no baje de 0 ni suba del m�ximo
        UpdateHealthUI();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("OnTriggerEnter called with: " + other.gameObject.name); // Mensaje de depuraci�n

        if (other.CompareTag("Zombie"))
        {
            Debug.Log("Player collided with Zombie"); // Mensaje de depuraci�n
            TakeDamage(damagePerHit);
        }
    }
}