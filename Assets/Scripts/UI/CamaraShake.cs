using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraShake : MonoBehaviour
{
    [Header("Shake Properties")]
    [SerializeField] private float shakeAmount = 0.1f; // Cantidad de movimiento de la cámara
    [SerializeField] private float shakeDuration = 0.5f; // Duración del movimiento de la cámara

    private Transform cameraTransform;
    private Vector3 originalPosition;
    private float shakeTime;

    private void Start()
    {
        cameraTransform = Camera.main.transform;
        originalPosition = cameraTransform.localPosition;
    }

    private void Update()
    {
        if (shakeTime > 0)
        {
            cameraTransform.localPosition = originalPosition + Random.insideUnitSphere * shakeAmount;
            shakeTime -= Time.deltaTime;
        }
        else
        {
            shakeTime = 0f;
            cameraTransform.localPosition = originalPosition;
        }
    }

    public void ShakeCamera(float duration)
    {
        shakeTime = duration;
    }
}