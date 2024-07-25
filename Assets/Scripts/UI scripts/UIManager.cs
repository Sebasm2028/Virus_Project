using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Health UI Elements")]
    [SerializeField] private Image healthImage;
    [SerializeField] private Image damageImage;

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
        playerStats = FindObjectOfType<PlayerStats>();

        // Suscribirse a los eventos del jugador
        playerStats.OnPlayerDamaged += UpdateHealthUI;
        playerStats.OnPlayerDamaged += ShowDamageEffect;
        playerStats.OnPlayerGetsAmmo += UpdateAmmoUI;

        // Inicializar la UI
        UpdateHealthUI(0);
        UpdateAmmoUI(0);
    }

    public void UpdateHealthUI(float damage)
    {
        float healthPercentage = playerStats.GetHP() / 100f; // Asumiendo que 100 es el valor máximo de salud
        healthImage.fillAmount = healthPercentage;

        // Si quieres animar el daño, puedes hacerlo aquí
        damageImage.fillAmount = Mathf.Lerp(damageImage.fillAmount, healthPercentage, 0.1f); // Ajusta el 0.1f para la velocidad de animación
    }

    public void UpdateAmmoUI(int ammo)
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
        AudioManager.Instance.PlayPlayerSound(AudioManager.Instance.playerSource.clip); // Reproduce el sonido de daño
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
        playerStats.OnPlayerDamaged -= UpdateHealthUI;
        playerStats.OnPlayerDamaged -= ShowDamageEffect;
        playerStats.OnPlayerGetsAmmo -= UpdateAmmoUI;
    }
}