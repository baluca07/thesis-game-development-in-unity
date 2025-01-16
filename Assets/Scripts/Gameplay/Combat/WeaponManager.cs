using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    private PlayerInventory playerInventory;
    public MeleeWeapon currentMeleeWeapon;
    public RangedWeapon currentRangedWeapon;
    public GameObject currentWeapon;

    public bool isAttacking = false;

    public WeaponType activeWeaponType;
    public Damage currentDamage;

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
            currentDamage = new Damage(currentMeleeWeapon.elementalDamageType,currentMeleeWeapon.baseDamage);
            
            activeWeaponType = currentMeleeWeapon.weaponType;
      
        }
        else if (currentWeapon.CompareTag("Ranged"))
        {
            currentRangedWeapon = currentWeapon.GetComponent<RangedWeapon>();
            currentMeleeWeapon = null;
            currentDamage = new Damage( currentRangedWeapon.elementalDamageType, currentRangedWeapon.baseDamage);

            activeWeaponType = currentRangedWeapon.weaponType;
        } 
        Debug.Log($"Equipped weapon:  {currentWeapon.name}");
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
