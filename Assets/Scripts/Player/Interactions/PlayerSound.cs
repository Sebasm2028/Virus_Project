using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] private AudioSource playerSource;

    [Header("Movement")]
    [SerializeField] private List<AudioClip> walkSounds;
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip landSound;

    [Header("Combat")]
    [SerializeField] private List<AudioClip> hurtSounds;
    [SerializeField] private AudioClip pistolEmptySound;
    [SerializeField] private AudioClip pistolShotSound;
    [SerializeField] private AudioClip knifeAttackSound;
    [SerializeField] private AudioClip knifeGrabbedSound;
    [SerializeField] private AudioClip pistolGrabbedSound;

    [Header("Script References")]
    [SerializeField] private PlayerMovement movement;
    [SerializeField] private PlayerStats stats;
    [SerializeField] private PlayerCombat combat;

    private void OnEnable()
    {
        movement.OnPlayerJump += OnPlayerJumped;
        movement.OnPlayerLand += OnPlayerLanded;

        stats.OnPlayerDamaged += OnPlayerHitted;

        combat.OnPlayerStartAttack += OnPlayerAttack;
        combat.OnPlayerWeaponChange += OnPlayerWeaponChanged;
    }

    private void OnDisable()
    {
        movement.OnPlayerJump -= OnPlayerJumped;
        movement.OnPlayerLand -= OnPlayerLanded;

        stats.OnPlayerDamaged -= OnPlayerHitted;
        combat.OnPlayerStartAttack -= OnPlayerAttack;
        combat.OnPlayerWeaponChange -= OnPlayerWeaponChanged;
    }

    private void Update()
    {
        Movement();
    }

    #region Movement

    private void Movement()
    {
        if (movement.isMoving() && movement.isGrounded())
        {
            if (!playerSource.isPlaying)
            {
                playerSource.PlayOneShot(walkSounds[Random.Range(0, walkSounds.Count)]);
            }
        }
    }

    private void OnPlayerJumped()
    {
        if (playerSource.isPlaying)
        {
            playerSource.Stop();
            playerSource.PlayOneShot(jumpSound);
        }
        else
        {
            playerSource.PlayOneShot(jumpSound);
        }
    }

    private void OnPlayerLanded()
    {
        if (!playerSource.isPlaying)
        {
            playerSource.PlayOneShot(landSound);
        }
    }

    #endregion

    #region Stats

    private void OnPlayerHitted(float damage)
    {
        if (playerSource.isPlaying)
        {
            playerSource.Stop();
            playerSource.PlayOneShot(hurtSounds[Random.Range(0, hurtSounds.Count)]);
        }
        else
        {
            playerSource.PlayOneShot(hurtSounds[Random.Range(0, hurtSounds.Count)]);
        }
    }

    #endregion

    #region Combat

    private void OnPlayerAttack(AttackType type)
    {
        if (type == AttackType.Melee)
            playerSource.PlayOneShot(knifeAttackSound);
        else if (type == AttackType.Fire)
        {
            if (stats.GetTotalAmmo() <= 0)
            {
                playerSource.PlayOneShot(pistolEmptySound);
            }        
            else
            {
                playerSource.PlayOneShot(pistolShotSound);
            }                
        }           
    }

    private void OnPlayerWeaponChanged(ActualWeapon weapon)
    {
        if (weapon == ActualWeapon.Knife)
            playerSource.PlayOneShot(knifeGrabbedSound);
        else if (weapon == ActualWeapon.Pistol)
            playerSource.PlayOneShot(pistolGrabbedSound);
    }

    #endregion
}

