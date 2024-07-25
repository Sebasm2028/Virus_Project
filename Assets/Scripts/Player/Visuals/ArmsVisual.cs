using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmsVisual : MonoBehaviour
{
    [Header("Animator Properties")]
    [SerializeField] private Animator animator;

    [Header("Arms Scale")]
    [SerializeField] private Transform parentTransform;

    [Header("Weapons References")]
    [SerializeField] private GameObject pistolGO;
    [SerializeField] private GameObject knifeGO;

    [Header("Particle Effects")]
    [SerializeField] private GameObject pistolAttackParticles;

    [Header("Script References")]
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerCombat combat;
    [SerializeField] private PlayerStats stats;
    [SerializeField] private UIManager uiManager;

    // Start is called before the first frame update
    void Start()
    {
        combat.OnPlayerStartAttack += OnPlayerAttack;
        combat.OnPlayerWeaponChange += OnWeaponChanged;
        stats.OnPlayerDamaged += OnPlayerGetDamaged;
        stats.OnPlayerGetsAmmo += OnPlayerGetAmmo;

        knifeGO.SetActive(true);
        animator.SetTrigger("Idle_Arms_Trigger");
    }
    private void OnDisable()
    {
        combat.OnPlayerStartAttack -= OnPlayerAttack;
        combat.OnPlayerWeaponChange -= OnWeaponChanged;
    }

    private void Update()
    {
        ArmsWalkAnim();
    }

    private void LateUpdate()
    {
        AdaptArmsScaleOnCrouching();
    }

    #region Arms Animations

    private void AdaptArmsScaleOnCrouching()
    {
        if (parentTransform != null)
        {
            Vector3 inverseScale = new Vector3(
                1f / parentTransform.localScale.x,
                1f / parentTransform.localScale.y,
                1f / parentTransform.localScale.z);

            transform.localScale = inverseScale;
        }       
    }

    private void ArmsWalkAnim()
    {
        animator.SetBool("isMoving", playerMovement.isMoving());
    }

    /// <summary>
    /// Triggers when player attack
    /// </summary>
    /// <param name="type"></param>
    private void OnPlayerAttack(AttackType type)
    {
        if (type == AttackType.Fire)
        {
            if (stats.GetAMMOInCartridge() > 0)
            {
                animator.SetTrigger("PistolAttack");

                if (pistolAttackParticles != null)
                {
                    // Instanciar las partículas en la posición del arma de fuego
                    Instantiate(pistolAttackParticles, pistolGO.transform.position, pistolGO.transform.rotation);
                }
            }
        }

        if (type == AttackType.Melee)
            animator.SetTrigger("KnifeAttack");

    }

    /// <summary>
    /// Triggers when player changes weapon
    /// </summary>
    /// <param name="weapon"></param>
    private void OnWeaponChanged(ActualWeapon weapon)
    {
        if (weapon == ActualWeapon.Pistol)
        {
            animator.SetTrigger("Idle_Pistol_Trigger");
            pistolGO.SetActive(true);
            knifeGO.SetActive(false);
        }

        if (weapon == ActualWeapon.Knife)
        {
            animator.SetTrigger("Idle_Arms_Trigger");
            knifeGO.SetActive(true);
            pistolGO.SetActive(false);
        }
    }

    #endregion

    #region Health

    /// <summary>
    /// Triggers when player get damaged
    /// </summary>
    /// <param name="damageReceived"></param>
    private void OnPlayerGetDamaged(float damageReceived)
    {

    }

    #endregion

    #region Ammo

    /// <summary>
    /// Triggers when player gets ammo
    /// </summary>
    /// <param name="ammoReceived"></param>
    private void OnPlayerGetAmmo(int ammoReceived)
    {
        
    }

    #endregion
}
