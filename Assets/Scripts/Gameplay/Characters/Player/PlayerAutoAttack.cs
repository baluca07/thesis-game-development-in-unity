using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AutoAttack : MonoBehaviour
{
    private bool isAim = false;
    private bool isAttack = false;
    private bool canAttack = true;

    private WeaponManager weaponManager;


    private void Start()
    {
        foreach (Transform child in transform)
        {
            if (child.gameObject.CompareTag("Weapon"))
            {
                weaponManager = child.gameObject.GetComponent<WeaponManager>();
            }
        }

        //weaponManager.UpdateWeapon();
    }
    private void Update()
    {
        canAttack = !isAttack;
        if (canAttack)
        {
            weaponManager.Attack();
        }
    }
}
