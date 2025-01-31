using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    Sword = 0,
    Wand = 1
}

public class Weapon : MonoBehaviour
{
    [Header("Manualy Managed")]
    [Header("Basic Stats")]
    public string weaponName;
    public int baseDamage;
    public float damageSpeed;
    public float attackCooldown;

    protected bool isOnCooldown = false;

    [Header("Damage Category")]
    public WeaponType weaponType;

    [Header("Damage Type")]
    public ElementalDamageType elementalDamageType;

    protected IEnumerator AttackCoolDown()
    {
        isOnCooldown = true;
        yield return new WaitForSeconds(attackCooldown);
        isOnCooldown = false;
    }

    public virtual void Attack() {}
}
