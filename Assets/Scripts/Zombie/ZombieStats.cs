using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieStats : MonoBehaviour
{
    [Header("Health Stats")]
    [SerializeField] private float healthPoints;

    [Header("Combat Stats")]
    [SerializeField] private float damagePoints;
    [SerializeField] private float attackSpeed;

    #region Eventos

    public event Action OnZombieDie;

    #endregion

    #region Getter / Setter

    public float GetHealthPoints() {  return healthPoints; }

    #endregion

    // Update is called once per frame
    void Update()
    {
        HealthManager();
    }

    #region Health

    public void Damage(float damage)
    {
        this.healthPoints -= damage;
    }

    private void HealthManager()
    {
        if (healthPoints <= 0)
        {
            OnZombieDie?.Invoke();
            this.enabled = false;
        }
    }

    #endregion

    [ContextMenu("test")]
    public void test()
    {
        Damage(5);
    }
}
