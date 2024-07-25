using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageEffect : MonoBehaviour
{
   
    [Header("UI References")]
    [SerializeField] private Image damageOverlay; // Imagen semi-transparente para el efecto de da�o
    [SerializeField] private float fadeDuration = 0.5f; // Duraci�n del desvanecimiento
    [SerializeField] private float damageColorAlpha = 0.5f; // Nivel de opacidad del color de da�o

    [Header("Camera Shake")]
    [SerializeField] private CamaraShake cameraShake; // Referencia al script de movimiento de la c�mara
    [SerializeField] private float shakeDuration = 0.5f; // Duraci�n del movimiento de la c�mara

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
        // Configura el color de da�o
        Color color = damageOverlay.color;
        color.a = damageColorAlpha; // Color de da�o con opacidad
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

        // Aseg�rate de que el overlay quede completamente transparente al final del desvanecimiento
        color.a = 0f;
        damageOverlay.color = color;
    }
}