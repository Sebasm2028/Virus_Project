using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAnimations : MonoBehaviour
{
    [Header("Animator Properties")]
    [SerializeField] private Animator animator;

    [Header("Script References")]
    [SerializeField] private ZombieMovement zombieMovement;
    [SerializeField] private ZombieStats stats;

    void Start()
    {
        stats.OnZombieDie += OnZombieDied;
        stats.OnZombieAttack += OnZombieAttacked;
    }

    private void OnDisable()
    {
        stats.OnZombieDie -= OnZombieDied;
        stats.OnZombieAttack -= OnZombieAttacked;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    #region Movement

    private void Movement()
    {
        if (zombieMovement.GetAgent().velocity.magnitude > 0.1f) animator.SetBool("isMoving", true);
        else animator.SetBool("isMoving", false);

        if (zombieMovement.GetTarget() != null) animator.SetBool("isFollowingPlayer", true);
        else animator.SetBool("isFollowingPlayer", false);
    }

    #endregion

    #region Attack

    private void OnZombieAttacked()
    {
        animator.SetInteger("AttackIndex", Random.Range(0, 4));
        animator.SetTrigger("Attack");
    }

    #endregion

    private void OnZombieDied()
    {
        animator.enabled = false;
        this.enabled = false;
    }

    [ContextMenu("Apply Ragdoll")]
    public void ApplyRagdoll()
    {
        animator.enabled = false;
        zombieMovement.GetAgent().isStopped = true;
    }
}
