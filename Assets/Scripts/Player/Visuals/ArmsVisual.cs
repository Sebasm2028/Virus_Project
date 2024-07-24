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

    [Header("Script References")]
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerCombat combat;

    // Start is called before the first frame update
    void Start()
    {
        combat.OnPlayerStartAttack += OnPlayerAttack;
        combat.OnPlayerWeaponChange += OnWeaponChanged;

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

    private void OnPlayerAttack(AttackType type)
    {
        if (type == AttackType.Fire)
            animator.SetTrigger("PistolAttack");

        if (type == AttackType.Melee)
            animator.SetTrigger("KnifeAttack");
    }

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
}
