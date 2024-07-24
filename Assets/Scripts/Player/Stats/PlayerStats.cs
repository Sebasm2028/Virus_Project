using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Health Properties")]
    [SerializeField] private float healthPoints;

    [Header("Weapons Properties")]
    [SerializeField] private int ammoInCartridge;
    [SerializeField] private int totalAmmo;

    #region Events

    public event Action<float> OnPlayerDamaged;

    public event Action<int> OnPlayerGetsAmmo;

    #endregion

    #region Getter / Setter

    public float GetHP() { return healthPoints; }

    public void SetHP(float hp) { this.healthPoints = hp; }

    public float GetAMMOInCartridge() { return ammoInCartridge; }

    public void SetAmmoInCartrige(int ammoInCartrige) { this.ammoInCartridge = ammoInCartrige; }

    public int GetTotalAmmo() { return totalAmmo; }

    private void SetTotalAmmo(int totalAmmo) { this.totalAmmo = totalAmmo; }

    #endregion

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region Health Points

    public void DamagePlayer(float damage)
    {
        this.healthPoints -= damage;
        OnPlayerDamaged?.Invoke(damage);
    }

    #endregion

    #region Ammo

    public void AddAmmo(int ammo)
    {
        this.totalAmmo += ammo;
        OnPlayerGetsAmmo?.Invoke(ammo);
    }

    #endregion
}
