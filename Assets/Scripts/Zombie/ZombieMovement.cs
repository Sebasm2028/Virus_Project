using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class ZombieMovement : MonoBehaviour
{
    [Header("Movement Properties")]
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Collider rbCollider;

    [Header("Nav Agent Properties")]
    [SerializeField] private Transform target;
    [SerializeField] private NavMeshAgent agent;

    [Header("Patrolling Properties")]
    [SerializeField] private float minRandomMovementDelay;
    [SerializeField] private float maxRandomMovementDelay;
    [SerializeField] private float minRandomMovementRange;
    [SerializeField] private float maxRandomMovementRange;
    bool goNextPoint = true;

    [Header("Player Interactions")]
    [SerializeField] private float distanceThreeshold;

    [Header("Script References")]
    [SerializeField] private ZombieStats stats;

    #region Getter / Setter

    public NavMeshAgent GetAgent() { return agent; }

    public Transform GetTarget() { return target; }

    public void SetTarget(Transform target) { this.target = target; }

    #endregion

    private void Start()
    {
        stats.OnZombieDie += OnZombieDied;
    }

    private void OnDisable()
    {
        stats.OnZombieDie -= OnZombieDied;
    }

    // Update is called once per frame
    void Update()
    {
        ForgetPlayerLocation();
        Movement();
    }

    #region Movement

    /// <summary>
    /// Move the NPC
    /// </summary>
    private void Movement()
    {
        if (agent != null)
        {
            if (target != null)
            {
                agent.speed = runSpeed;
                agent.destination = target.position;
                // Calcula la nueva posición y rotación basadas en el agente
                Vector3 newPosition = agent.transform.position;
                Quaternion newRotation = agent.transform.rotation;

                // Mueve el Rigidbody a la nueva posición
                rb.MovePosition(newPosition);
                //rb.MoveRotation(newRotation);
            }
            else
            {
                agent.speed = walkSpeed;

                if (agent.remainingDistance <= agent.stoppingDistance && goNextPoint)
                {
                    agent.SetDestination(GenerateRandomPoint(transform.position, Random.Range(minRandomMovementRange, maxRandomMovementRange)));
                    goNextPoint = false;
                    StartCoroutine(RandomPointEnumerator());
                }
            }
        }
    }

    private IEnumerator RandomPointEnumerator()
    {
        yield return new WaitForSeconds(Random.Range(minRandomMovementDelay, maxRandomMovementDelay));
        goNextPoint = true;
    }

    private Vector3 GenerateRandomPoint(Vector3 position, float range)
    {
        Vector3 randomPoint = position + Random.insideUnitSphere * range; //Generate a random point in a sphere
        NavMeshHit hit;

        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
        {
            return hit.position;
        }

        return Vector3.zero;
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

    #region Checkers

    public bool ArrivedToPlayer()
    {
        if (target != null && agent.hasPath)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (agent.velocity.sqrMagnitude <= 0.25f)
                {                  
                    return true;
                }

                return false;
            }
        }

        return false;
    }

    #endregion

    private void OnZombieDied()
    {
        agent.isStopped = true;

        rbCollider.enabled = false;
        this.enabled = false;
    }
}
