using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    private WeaponManager weaponManager;


    private void Start()
    {
        foreach(Transform child in transform)
        {
            if (child.gameObject.CompareTag("Weapon"))
            {
                weaponManager = child.gameObject.GetComponent<WeaponManager>();
            }
        }
        if (weaponManager == null)
        {
            Debug.LogError("WeaponManager is missing on the player.");
            return;
        }

        //weaponManager.UpdateWeapon();
    }

    public void OnAim(InputAction.CallbackContext context)
    {
        /*
            //Aim Spell Logic
            if (context.started)
            {
                isAim = true;
                //Debug.Log("Aim started");
                //Debug.Log("Aim started");
            }
            else if (context.canceled)
            {
                isAim = false;
                //Debug.Log("Aim canceled");
            }
        */
        Debug.LogWarning("Aim Spell: not implemented yet");
    }



    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            weaponManager.Attack();
        }
    }
}
