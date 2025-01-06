using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    public MeleeWeaponData weaponData;

    [Header("Basic Stats")]
    public string weaponName;
    public int baseDamage;
    public int damageSpeed;

    [Header("Damage Category")]
    public DamageCategory damageCategory;

    [Header("Damage Type")]
    public ElementalDamageType elementalDamageType;

    public PlayerCombat playerCombat;

    void Start()
    {
        playerCombat = GetComponentInParent<PlayerCombat>();
        if (weaponData != null)
        {
            Debug.Log("Weapon: " + weaponData.weaponName);
            weaponName = weaponData.weaponName;
            baseDamage = weaponData.baseDamage;
            damageSpeed = weaponData.damageSpeed;
            damageCategory = weaponData.damageCategory;
            elementalDamageType = weaponData.elementalDamageType;
        }
        else
        {
            Debug.LogWarning("No WeaponData assigned to this weapon!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();

        if (enemy != null && playerCombat != null)
        {
            playerCombat.AttackEnemy(enemy);
        }
    }
}
