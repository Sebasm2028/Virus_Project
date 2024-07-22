using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ZombieAI : MonoBehaviour
{
    [Header("AI Field of View")]
    [SerializeField][Range(0, 360)] private float radiusFOV;
    [SerializeField] private float angle;
    [SerializeField] private LayerMask obstacleMask;
    [SerializeField] private LayerMask targetMask;
    [SerializeField] private bool canSeePlayer;
    [SerializeField] private Transform playerRef;

    [Header("Script References")]
    [SerializeField] private ZombieMovement zombieMovement;

    private void Start()
    {
        StartCoroutine(FOVRoutine());
    }

    #region AI Field of View

    /// <summary>
    /// Check every 0.2 seconds if player enters in AI Field of View
    /// </summary>
    /// <returns></returns>
    private IEnumerator FOVRoutine()
    {
        WaitForSeconds seconds = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return seconds;
            FieldOfViewCheck();
        }
    }

    /// <summary>
    /// Detect player if enters in AI Field of View
    /// </summary>
    private void FieldOfViewCheck()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radiusFOV, targetMask);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstacleMask))
                {
                    canSeePlayer = true;
                    playerRef = target;
                    zombieMovement.SetTarget(playerRef);
                }
                else
                    canSeePlayer = false;
            }
            else
                canSeePlayer = false;
        }
        else if (canSeePlayer)
            canSeePlayer = false;
    }

    #endregion

    #region Debug

    private void OnDrawGizmos()
    {
        // Draw the view radius
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, radiusFOV);

        // Draw the view angles
        Vector3 viewAngle01 = DirectionFromAngle(transform.eulerAngles.y, -angle / 2);
        Vector3 viewAngle02 = DirectionFromAngle(transform.eulerAngles.y, angle / 2);

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + viewAngle01 * radiusFOV);
        Gizmos.DrawLine(transform.position, transform.position + viewAngle02 * radiusFOV);

        // Draw the line to the player if the player is in view
        if (canSeePlayer && playerRef != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, playerRef.position);
        }
    }

    private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }


    #endregion
}
