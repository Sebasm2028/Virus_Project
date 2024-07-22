using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieMovement : MonoBehaviour
{
    [Header("Movement Properties")]
    [SerializeField] private Rigidbody rb;

    [Header("Nav Agent Properties")]
    [SerializeField] private Transform target;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private float distanceThreeshold;

    #region Getter / Setter

    public NavMeshAgent GetAgent() { return agent; }

    public Transform GetTarget() { return target; }
    public void SetTarget(Transform target) { this.target = target; }

    #endregion

    // Update is called once per frame
    void Update()
    {
        ForgetPlayerLocation();
        Movement();
    }

    #region Movement

    private void Movement()
    {
        if (agent != null)
        {
            if (target != null)
            {
                agent.destination = target.position;
                // Calcula la nueva posición y rotación basadas en el agente
                Vector3 newPosition = agent.transform.position;
                Quaternion newRotation = agent.transform.rotation;

                // Mueve el Rigidbody a la nueva posición
                rb.MovePosition(newPosition);
                //rb.MoveRotation(newRotation);
            }
        }
    }

    #endregion

    #region Field of View

    private void ForgetPlayerLocation()
    {
        if (target != null)
        {
            if (Vector3.Distance(agent.transform.position, target.position) > distanceThreeshold)
            {
                target = null;
            }
        }
    }

    #endregion
}
