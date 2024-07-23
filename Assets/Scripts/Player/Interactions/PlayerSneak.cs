using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSneak : MonoBehaviour
{
    [Header("Player Footstep Properties")]
    [SerializeField] private float movementThreeshold;
    [SerializeField] private float crouchNoise;
    [SerializeField] private float walkNoise;
    [SerializeField] private float runNoise;
    [SerializeField] private float actualNoise;
    [SerializeField] private Transform footTransform;
    [SerializeField] private LayerMask enemyLayerMask;

    [Header("Script References")]
    [SerializeField] private PlayerMovement playerMovement;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        NoiseManager();
        TriggerZombies();
    }

    private void TriggerZombies()
    {
        if (playerMovement.GetPlayerRB().velocity.magnitude > movementThreeshold)
        {
            Collider[] colliders = Physics.OverlapSphere(footTransform.position, actualNoise, enemyLayerMask);

            foreach (Collider collider in colliders)
            {
                ZombieMovement zombieMovement = collider.gameObject.GetComponent<ZombieMovement>();

                if (zombieMovement != null)
                {
                    zombieMovement.SetTarget(transform);
                }
            }
        }
    }

    private void NoiseManager()
    {
        switch(playerMovement.GetMovementState())
        {
            case MovementState.crouching:
                actualNoise = crouchNoise;
                break;
            case MovementState.walking:
                actualNoise = walkNoise;
                break;
            case MovementState.sprinting:
                actualNoise = runNoise;
                break;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(footTransform.position, actualNoise);
    }
}
