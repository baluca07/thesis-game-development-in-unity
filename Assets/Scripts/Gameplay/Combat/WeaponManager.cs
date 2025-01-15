using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{

    private List<GameObject> weapons = new List<GameObject>();
    [SerializeField] private MeleeWeapon currentMeleeWeapon;
    [SerializeField] private RangedWeapon currentRangedWeapon;
    public static bool isAnyWeaponActive = false;

    public static WeaponType activeWeaponType;
    private void Start()
    {
        GetWeapons();
        UpdateWeapon();
    }

    public void UpdateWeapon()
    {
        if (weapons.Count != 0)
        {
            GameObject activeWeapon = FindActiveWeapon();

            if (activeWeapon != null)
            {
                if (activeWeapon.CompareTag("Melee"))
                {
                    currentMeleeWeapon = activeWeapon.GetComponent<MeleeWeapon>();
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
                else if (activeWeapon.CompareTag("Ranged"))
                {
                    currentRangedWeapon = activeWeapon.GetComponent<RangedWeapon>();
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
                activeWeaponType = currentMeleeWeapon.weaponType;
                isAnyWeaponActive = true;
            }
            else
            {
                // No active weapon found
                isAnyWeaponActive = false;
                Debug.LogWarning("No active weapon found.");
            }
        }
        else
        {
            Debug.LogWarning("No weapons found in the list.");
            isAnyWeaponActive = false;
        }
    }

    private GameObject FindActiveWeapon()
    {
        foreach (GameObject weapon in weapons)
        {
            if (weapon.activeSelf)
            {
                return weapon;
            }
        }
        return null;
    }

    private void GetWeapons()
    {
        weapons.Clear(); // Clear the list to avoid duplicate entries

        foreach (Transform child in transform)
        {
            if (child.CompareTag("Melee") || child.CompareTag("Ranged"))
            {
                weapons.Add(child.gameObject);
            }
        }

        Debug.Log($"Found {weapons.Count} weapons under WeaponManager.");
    }

    public MeleeWeapon GetMeleeWeapon() => currentMeleeWeapon;
    public RangedWeapon GetRangedWeapon() => currentRangedWeapon;
}
