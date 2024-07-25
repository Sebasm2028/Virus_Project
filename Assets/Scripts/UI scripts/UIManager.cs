using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Health UI Elements")]
    [SerializeField] private Image healthImage;

    [Header("Ammo UI Elements")]
    [SerializeField] private TextMeshProUGUI ammoText;
    [SerializeField] private Image ammoImage; // Opcional si usas una barra de munición

    [Header("Damage Effect")]
    [SerializeField] private Image damageOverlay; // Imagen superpuesta para el efecto de daño
    [SerializeField] private Color overlayColor = new Color(0.5f, 0, 0, 0.5f);
    [SerializeField] private float overlayFadeDuration = 1.0f;

    private PlayerStats playerStats;

    private void Start()
    {
        // Encuentra el componente PlayerStats en el jugador
        playerStats = GetComponentInParent<PlayerStats>();

        // Suscribirse a los eventos del jugador
        playerStats.OnPlayerDamaged += ShowDamageEffect;

        // Inicializar la UI
    }

    private void Update()
    {
        UpdateHealthUI();
        UpdateAmmoUI();
    }

    public void UpdateHealthUI()
    {
        float healthPercentage = playerStats.GetHP() / playerStats.GetMaxHealth();
        healthImage.fillAmount = healthPercentage;
    }

    public void UpdateAmmoUI()
    {
        ammoText.text = playerStats.GetAMMOInCartridge().ToString() + " / " + playerStats.GetTotalAmmo().ToString();

        // Si usas una barra de munición
        if (ammoImage != null)
        {
            float ammoPercentage = (float)playerStats.GetAMMOInCartridge() / 100f; // Ajusta el 100 según el valor máximo de munición en el cartucho
            ammoImage.fillAmount = ammoPercentage;
        }
    }

    private void ShowDamageEffect(float damage)
    {
        StartCoroutine(FadeDamageOverlay());
    }

    private IEnumerator FadeDamageOverlay()
    {
        damageOverlay.color = overlayColor;
        float elapsedTime = 0f;

        while (elapsedTime < overlayFadeDuration)
        {
            elapsedTime += Time.deltaTime;
            damageOverlay.color = Color.Lerp(overlayColor, Color.clear, elapsedTime / overlayFadeDuration);
            yield return null;
        }

        damageOverlay.color = Color.clear;
    }

    private void OnDestroy()
    {
        // Desuscribirse de los eventos cuando el objeto se destruye
        playerStats.OnPlayerDamaged -= ShowDamageEffect;
    }
}