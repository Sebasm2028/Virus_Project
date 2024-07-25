using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBox : MonoBehaviour
{
    public int ammo = 10;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerStats stats = other.gameObject.GetComponent<PlayerStats>();

            if (stats != null)
            {
                stats.AddAmmo(ammo);
                Destroy(gameObject);
            }
        }
    }
}
