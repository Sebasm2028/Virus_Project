using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageEffect : MonoBehaviour
{
   
    [Header("UI References")]
    [SerializeField] private Image damageOverlay; // Imagen semi-transparente para el efecto de daño
    [SerializeField] private float fadeDuration = 0.5f; // Duración del desvanecimiento
    [SerializeField] private float damageColorAlpha = 0.5f; // Nivel de opacidad del color de daño

    [Header("Camera Shake")]
    [SerializeField] private CamaraShake cameraShake; // Referencia al script de movimiento de la cámara
    [SerializeField] private float shakeDuration = 0.5f; // Duración del movimiento de la cámara

    private void Start()
    {
        // Inicialmente el overlay debe estar completamente transparente
        if (damageOverlay != null)
        {
            Color color = damageOverlay.color;
            color.a = 0f;
            damageOverlay.color = color;
        }
    }

    public void TriggerDamageEffect()
    {
        if (damageOverlay != null)
        {
            StartCoroutine(FadeDamageOverlay());
        }

        if (cameraShake != null)
        {
            cameraShake.ShakeCamera(shakeDuration);
        }
    }

    private IEnumerator FadeDamageOverlay()
    {
        // Configura el color de daño
        Color color = damageOverlay.color;
        color.a = damageColorAlpha; // Color de daño con opacidad
        damageOverlay.color = color;

        // Desvanecimiento del overlay
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            color.a = Mathf.Lerp(damageColorAlpha, 0f, elapsedTime / fadeDuration);
            damageOverlay.color = color;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Asegúrate de que el overlay quede completamente transparente al final del desvanecimiento
        color.a = 0f;
        damageOverlay.color = color;
    }
}