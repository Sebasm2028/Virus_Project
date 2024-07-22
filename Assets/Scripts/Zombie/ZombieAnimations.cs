using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAnimations : MonoBehaviour
{
    [Header("Animator Properties")]
    [SerializeField] private Animator animator;

    [Header("Script References")]
    [SerializeField] private ZombieMovement zombieMovement;

    void Start()
    {
        
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
    }

    #endregion
}
