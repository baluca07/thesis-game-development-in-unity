using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    private bool isAim = false;
    private bool isMeleeAttack = false;

    private Vector2 attackDirection;
    private float attackAngle;


    [SerializeField] GameObject weapon;

    [SerializeField] MeleeWeapon currentMeleeWeapon;


    private void Start()
    {
        weapon = FindWeapon();

        if (weapon == null)
        {
            Debug.LogWarning("Weapon not found! Make sure the Weapon object is tagged as 'Weapon'.");
        }
        else
        {
            Debug.Log("Weapon found.");
        }

        UpdateMeleeWeapon();

    }
    private void Update()
    {

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        attackDirection = (mousePosition - transform.position).normalized;

        if (isAim)
        {
            SetAttackRotation(transform);

            Debug.DrawLine(transform.position, mousePosition, Color.red);
        }
    }

    public void OnAim(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isAim = true;
            Debug.Log("Aim started");
            Debug.Log("Aim started");
        }
        else if (context.canceled)
        {
            isAim = false;
            Debug.Log("Aim canceled");
        }
    }

    public void OnMeleeAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            SetAttackRotation(weapon.transform);
            currentMeleeWeapon.PerformMeleeAttack(ref isMeleeAttack);
        }
    }

    public void UpdateMeleeWeapon()
    {
        if (weapon == null)
        {
            Debug.LogWarning("Weapon is not assigned, cannot find melee weapon.");
            return;
        }

        GameObject meleeWeapon = FindMeleeWeapon();

        if (meleeWeapon != null)
        {
            currentMeleeWeapon = meleeWeapon.GetComponent<MeleeWeapon>();
            if (currentMeleeWeapon != null)
            {
                Debug.Log($"Weapon Damage: {currentMeleeWeapon.baseDamage}");
            }
            else
            {
                Debug.LogWarning("MeleeWeapon script not found on the Melee object.");
            }
        }
        else
        {
            Debug.LogWarning("MeleeWeapon object not found!");
        }
    }
    private GameObject FindWeapon()
    {
        foreach (Transform child in transform)
        {
            if (child.CompareTag("Weapon"))
            {
                return child.gameObject;
            }
        }
        return null;
    }

    private GameObject FindMeleeWeapon()
    {
        foreach (Transform child in weapon.transform)
        {
            if (child.CompareTag("Melee"))
            {
                return child.gameObject;
            }
        }
        return null;
    }

    private void SetAttackRotation(Transform weaponTransform)
    {
        // Calculate attack angle
        float attackAngle = Mathf.Atan2(attackDirection.y, attackDirection.x) * Mathf.Rad2Deg;

        // Determine the character's facing rotation
        float characterFacingYRotation = transform.rotation.eulerAngles.y;

        if (characterFacingYRotation == 0)
        {
            attackAngle = Mathf.Clamp(attackAngle, -90f, 90f);
        }
        else if (Mathf.Approximately(characterFacingYRotation, 180f))
        {
            if (attackAngle > 0)
                attackAngle = Mathf.Clamp(attackAngle, 90f, 180f);
            else
                attackAngle = Mathf.Clamp(attackAngle, -180f, -90f);
        }

        weaponTransform.rotation = Quaternion.Euler(0, 0, attackAngle);
    }




}
