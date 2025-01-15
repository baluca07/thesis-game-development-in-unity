using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    private PlayerInventory playerInventory;
    [SerializeField] MeleeWeapon currentMeleeWeapon;
    [SerializeField] RangedWeapon currentRangedWeapon;
    [SerializeField] GameObject currentWeapon;

    public static WeaponType activeWeaponType;

    private void Start()
    {
        playerInventory = GetComponent<PlayerInventory>();
    }

    public void EquipWeapon(GameObject newWeapon)
    {
        if (currentWeapon != null)
        {
            Destroy(currentWeapon.gameObject);
        }

        currentWeapon = Instantiate(newWeapon, transform);

        if (currentWeapon.CompareTag("Melee"))
        {
            currentMeleeWeapon = currentWeapon.GetComponent<MeleeWeapon>();
            currentRangedWeapon = null;

            if (currentMeleeWeapon != null)
            {
                activeWeaponType = currentMeleeWeapon.weaponType;
            }
            else
            {
                Debug.LogError("Melee weapon component missing on active weapon!");
            }
        }
        else if (currentWeapon.CompareTag("Ranged"))
        {
            currentRangedWeapon = currentWeapon.GetComponent<RangedWeapon>();
            currentMeleeWeapon = null;

            if (currentRangedWeapon != null)
            {
                activeWeaponType = currentRangedWeapon.weaponType;
            }
            else
            {
                Debug.LogError("Ranged weapon component missing on active weapon!");
            }
        }
    }
    public void Attack()
    {
        if (currentMeleeWeapon != null)
        {
            currentMeleeWeapon.Attack();
        }
        else if (currentRangedWeapon != null)
        {
            currentRangedWeapon.Attack();
        }
        else
        {
            Debug.Log("No weapon equipped!");
        }
    }
}
