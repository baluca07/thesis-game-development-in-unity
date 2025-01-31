using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public List<GameObject> weapons = new List<GameObject>();
    [SerializeField] WeaponManager weaponManager;

    public int currentIndex = 0;

    private void Start()
    {
        foreach (Transform child in transform)
        {
            if (child.gameObject.CompareTag("Weapon"))
            {
                weaponManager = child.gameObject.GetComponent<WeaponManager>();
            }
        }
        weaponManager.EquipWeapon(weapons[0]);
    }

    private void Update()
    {
        //weaponManager.EquipWeapon(weapons[currentIndex]);
    }

    private void CycleWeapon()
    {
        currentIndex = (currentIndex + 1) % weapons.Count;
        weaponManager.EquipWeapon(weapons[currentIndex]);
    }
}

