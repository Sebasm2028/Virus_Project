using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHitbox : MonoBehaviour
{
    [Header("Damage Properties")]
    [SerializeField] private float damageMultiplier;

    [Header("Script References")]
    [SerializeField] private ZombieStats stats;

    public void ApplyDamage(float damage)
    {
        stats.Damage(damage * damageMultiplier);
        Debug.Log($"Base Damage: {damage} * {damageMultiplier}, FinalDamage: {damage * damageMultiplier}");
    }
}
