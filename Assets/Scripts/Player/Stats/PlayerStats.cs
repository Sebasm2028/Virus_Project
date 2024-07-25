using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Health Properties")]
    [SerializeField] private float healthPoints;
    [SerializeField] private float maxHealthPoints;
    [SerializeField] private float healPerSecond = 2f; // Sanación por segundo
    [SerializeField] private float regenDelay;
    public bool isDeath = false;
    private bool canRegen = true;

    [Header("Weapons Properties")]
    [SerializeField] private int totalAmmo;

    [Header("Damage Effect")]
    [SerializeField] private DamageEffect damageEffect;

    #region Events

    public event Action<float> OnPlayerDamaged;

    public event Action<int> OnPlayerGetsAmmo;

    public event Action OnPlayerDie;

    #endregion

    #region Getter / Setter

    public float GetHP() { return healthPoints; }


    public void SetHP(float hp) { this.healthPoints = hp; }

    public float GetMaxHealth() { return maxHealthPoints; }

    public void SetMaxHealth() { this.maxHealthPoints = healthPoints; }

    public int GetTotalAmmo() { return totalAmmo; }

    private void SetTotalAmmo(int totalAmmo) { this.totalAmmo = totalAmmo; }

    #endregion

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (healthPoints <= 0) isDeath = true;
        RegenHP();
    }

    #region Health Points

    public void DamagePlayer(float damage)
    {
        healthPoints -= damage;
        healthPoints = Mathf.Clamp(healthPoints, 0, maxHealthPoints);

        // Activar el efecto de daño
        if (damageEffect != null)
        {
            damageEffect.TriggerDamageEffect();
        }

        canRegen = false;
        StartCoroutine(RegenEnumerator());
        OnPlayerDamaged?.Invoke(healthPoints);
    }

    private void RegenHP()
    {
        // Lógica de sanación gradual
        if (healthPoints < maxHealthPoints && canRegen)
        {
            healthPoints += healPerSecond * Time.deltaTime;
            healthPoints = Mathf.Clamp(healthPoints, 0, maxHealthPoints);
        }
    }

    private IEnumerator RegenEnumerator()
    {
        yield return new WaitForSeconds(regenDelay);
        canRegen = true;
    }

    #endregion

    #region Ammo

    public void AddAmmo(int ammo)
    {
        this.totalAmmo += ammo;
        OnPlayerGetsAmmo?.Invoke(ammo);//
    }

    public void RemoveAmmo(int ammo) { this.totalAmmo -= ammo; }

    #endregion
}
