using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ViewBobbing : MonoBehaviour
{
    [Header("ViewBobbing Properties")]
    [SerializeField] private float EffectIntensity;
    [SerializeField] private float EffectSpeed;
    [SerializeField] private float smoothFinalization;

    [Header("Multipliers")]
    [SerializeField] private float runMultiplier;
    [SerializeField] private float handMultiplier;
    [SerializeField] private float itemMultiplier;

    [Header("Referencies")]
    [SerializeField] private PlayerMovement playerMovement;
    private Vector3 OriginalOffset;
    private float SinTime;
    private float sinAmountY;

    private void Awake()
    {
        OriginalOffset = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerMovement.isMoving())
        {
            SinTime += Time.deltaTime * EffectSpeed;
        }
        else
        {
            SinTime = 0;
        }

        sinAmountY = -Mathf.Abs(getEffectIntensity() * Mathf.Sin(getSinTime()));

        float Offset = OriginalOffset.z + sinAmountY;

        if (playerMovement.isMoving() && playerMovement.isGrounded())
        {
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, Offset);
        }
        else
        {
            var newY = Mathf.Lerp(transform.localScale.z, OriginalOffset.z, smoothFinalization * Time.deltaTime);
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, newY);
        }
    }

    private float getEffectIntensity()
    {
        float newEffectIntensity = 0;

        newEffectIntensity = EffectIntensity * handMultiplier;

        return newEffectIntensity;
    }

    private float getSinTime()
    {
        float newSinTime = 0;

        if (playerMovement.GetIsSprinting()) newSinTime = SinTime * runMultiplier;
        else newSinTime = SinTime;

        return newSinTime;
    }
}
