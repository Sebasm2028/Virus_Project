using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [Header("Combat Properties")]
    [SerializeField] private float playerDamage;
    [SerializeField] private AttackType attackType;
    [SerializeField] private LayerMask zombieLayer;

    [Header("Melee Properties")]
    [SerializeField] private float meleeReach;

    [Header("Player Shooting Properties")]
    [SerializeField] private Transform raycastPoint;

    [Header("Player Attack Cooldown")]
    [SerializeField] private float meleeAttackCooldown;
    [SerializeField] private bool canMelee = true;
    [SerializeField] private float fireShootCooldown;
    [SerializeField] private bool canFireShoot = true;

    [Header("Weapon Swap Properties")]
    [SerializeField] private float weaponSwapCooldown;
    [SerializeField] private ActualWeapon weapon = ActualWeapon.Knife;
    [SerializeField] private bool canSwapWeapon = true;

    [Header("Layers Properties")]
    [SerializeField] private LayerMask defaultLayers;
    [SerializeField] private LayerMask hitboxLayer;

    private PlayerControls playerControl;

    #region Events

    public event Action<AttackType> OnPlayerStartAttack;
    public event Action<ActualWeapon> OnPlayerWeaponChange;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        playerControl = new PlayerControls();
        playerControl.Inventory.Enable();
        playerControl.Interactions.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeWeapons();

        if (playerControl.Interactions.Attack.IsPressed())
        {
            Shoot();
        }    
    }

    private void ChangeWeapons()
    {
        if (playerControl.Inventory.Pistol.WasPerformedThisFrame() && canSwapWeapon && weapon != ActualWeapon.Pistol)
        {
            canSwapWeapon = false;
            weapon = ActualWeapon.Pistol;
            attackType = AttackType.Fire;
            OnPlayerWeaponChange?.Invoke(weapon);
            StartCoroutine(swapWeaponEnumerator());
        }

        if (playerControl.Inventory.Knife.WasPerformedThisFrame() && canSwapWeapon && weapon != ActualWeapon.Knife)
        {
            canSwapWeapon = false;
            weapon = ActualWeapon.Knife;
            attackType = AttackType.Melee;
            OnPlayerWeaponChange?.Invoke(weapon);
            StartCoroutine(swapWeaponEnumerator());
        }
    }

    private void Shoot()
    {      
        if (attackType == AttackType.Fire && canFireShoot)
        {
            OnPlayerStartAttack?.Invoke(attackType);
            canFireShoot = false;
            StartCoroutine(fireAttackEnumerator());

            if (Physics.Raycast(raycastPoint.position, raycastPoint.transform.forward, out RaycastHit hit, Mathf.Infinity, (defaultLayers | hitboxLayer)))
            {
                Debug.Log("Fire attack aplied");
                ZombieHitbox zombieHitbox = hit.collider.gameObject.GetComponent<ZombieHitbox>();
                if (zombieHitbox != null)
                {
                    ZombieStats stats = hit.collider.GetComponentInParent<ZombieStats>();
                    if (stats.GetHealthPoints() > 0)
                        zombieHitbox.ApplyDamage(playerDamage);
                }
                Debug.DrawLine(raycastPoint.position, hit.point, Color.red, 3);
            }
        }

        if (attackType == AttackType.Melee && canMelee)
        {
            OnPlayerStartAttack?.Invoke(attackType);
            canMelee = false;
            StartCoroutine(meleeAttackEnumerator());

            if (Physics.Raycast(raycastPoint.position, raycastPoint.transform.forward, out RaycastHit hit, meleeReach, (defaultLayers | hitboxLayer)))
            {
                Debug.Log("Meelee attack aplied");
                ZombieHitbox zombieHitbox = hit.collider.gameObject.GetComponent<ZombieHitbox>();
                if (zombieHitbox != null)
                {
                    ZombieStats stats = hit.collider.GetComponentInParent<ZombieStats>();
                    if (stats.GetHealthPoints() > 0)
                        zombieHitbox.ApplyDamage(playerDamage);
                }
                Debug.DrawLine(raycastPoint.position, hit.point, Color.red, 3);
            }
        }
    }

    #region Cooldowns

    private IEnumerator meleeAttackEnumerator()
    {
        yield return new WaitForSeconds(meleeAttackCooldown);
        canMelee = true;        
    }

    private IEnumerator fireAttackEnumerator()
    {
        yield return new WaitForSeconds(fireShootCooldown);
        canFireShoot = true;
    }

    private IEnumerator swapWeaponEnumerator()
    {
        yield return new WaitForSeconds(weaponSwapCooldown);
        canSwapWeapon = true;
    }

    #endregion

}
