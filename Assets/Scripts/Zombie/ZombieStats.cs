using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieStats : MonoBehaviour
{
    [Header("Health Stats")]
    [SerializeField] private float healthPoints;
    [SerializeField] private Transform attackPosition;
    [SerializeField] private float attackRadius;

    [Header("Combat Stats")]
    [SerializeField] private float damagePoints;
    [SerializeField] private float attackSpeed;
    [SerializeField] private bool canAttack = true;
    [SerializeField] private LayerMask playerMask;

    [Header("Script References")]
    [SerializeField] private ZombieMovement zombieMovement;

    #region Eventos

    public event Action OnZombieDie;
    public event Action OnZombieAttack;
    public event Action OnZombieDamaged;

    #endregion

    #region Getter / Setter

    public float GetHealthPoints() {  return healthPoints; }

    #endregion

    // Update is called once per frame
    void Update()
    {
        HealthManager();
        Attack();
    }

    #region Health

    public void Damage(float damage)
    {
        this.healthPoints -= damage;
        OnZombieDamaged?.Invoke();
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

    #region Attack

    private void Attack()
    {
        if (zombieMovement.ArrivedToPlayer() && canAttack)
        {

            Collider[] colliders = Physics.OverlapSphere(attackPosition.position, attackRadius, playerMask);

            if (colliders != null)
            {
                if (colliders.Length > 0 &&colliders[0] != null)
                {
                    PlayerStats playerStats = colliders[0].gameObject.GetComponent<PlayerStats>();

                    if (playerStats != null)
                    {
                        canAttack = false;
                        OnZombieAttack?.Invoke();
                        playerStats.DamagePlayer(damagePoints);
                        StartCoroutine(attackCoroutine());
                    }

                }
            }
        }
    }

    private IEnumerator attackCoroutine()
    {
        yield return new WaitForSeconds(attackSpeed);

        canAttack = true;
    }

    #endregion

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(attackPosition.position, attackRadius);
    }
}
