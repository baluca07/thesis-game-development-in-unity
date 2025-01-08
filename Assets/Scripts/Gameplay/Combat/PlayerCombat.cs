using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    private bool isAim = false;
    private bool isMeleeAttack = false;

    public Vector2 attackDirection;

    public MeleeWeapon currentWeapon;


    private void Start()
    {

        UpdateWeaponCollider();

    }
    private void Update()
    {

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        attackDirection = (mousePosition - transform.position).normalized;

        if (isAim)
        {

            float angle = Mathf.Atan2(attackDirection.y, attackDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);

            Debug.DrawLine(transform.position, mousePosition, Color.red);
        }

        else if (isMeleeAttack)
        {
            float angle = Mathf.Atan2(attackDirection.y, attackDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);

            Debug.DrawLine(transform.position, (Vector2)transform.position + attackDirection * 10, Color.blue);
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
            currentWeapon.PerformMeleeAttack(ref isMeleeAttack,attackDirection);
        }
    }

    public void UpdateWeaponCollider()
    {
        GameObject meleeWeapon = FindMeleeWeapon();

        if (meleeWeapon != null)
        {
            currentWeapon = meleeWeapon.GetComponent<MeleeWeapon>();
            Debug.Log($"Weapon Damage: {currentWeapon.baseDamage}");
        }
        else
        {
            Debug.LogWarning("MeleeWeapon not found!");
        }
    }

    private GameObject FindMeleeWeapon()
    {
        foreach (Transform child in transform)
        {
            if (child.CompareTag("Melee"))
            {
                return child.gameObject;
            }
        }

        return null;
    }

}
