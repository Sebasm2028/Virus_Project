using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [Header("Player Shooting Properties")]
    [SerializeField] private Transform raycastPoint;

    [Header("Layers Properties")]
    [SerializeField] private LayerMask defaultLayers;
    [SerializeField] private LayerMask hitboxLayer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
    }

    private void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (Physics.Raycast(raycastPoint.position, raycastPoint.transform.forward, out RaycastHit hit, Mathf.Infinity, (defaultLayers | hitboxLayer)))
            {
                Debug.Log(hit.collider.gameObject.name);
                ZombieHitbox zombieHitbox = hit.collider.gameObject.GetComponent<ZombieHitbox>();
                if (zombieHitbox != null)
                {
                    ZombieStats stats = hit.collider.GetComponentInParent<ZombieStats>();
                    if (stats.GetHealthPoints() > 0)
                        zombieHitbox.ApplyDamage(1);
                }
                Debug.DrawLine(raycastPoint.position, hit.point, Color.red, 3);
            }
        }
    }
}
