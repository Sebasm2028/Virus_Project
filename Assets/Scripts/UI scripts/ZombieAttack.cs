using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAttack : MonoBehaviour
{
    public float damageAmount = 5f; // Cantidad de daño infligido por el zombie
    private HealthManager playerHealthManager; // Referencia al HealthManager del jugador

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerHealthManager = collision.gameObject.GetComponent<HealthManager>();
            if (playerHealthManager != null)
            {
                playerHealthManager.TakeDamage(damageAmount);
            }
        }
    }
}
