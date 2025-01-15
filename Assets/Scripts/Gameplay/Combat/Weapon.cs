using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public DamageCategory damageCategory;
    public WeaponType weaponType;

    [Header("Damage Type")]
    public ElementalDamageType elementalDamageType;

    public Damage damage;

    protected IEnumerator AttackCoolDown()
    {
        isOnCooldown = true;
        yield return new WaitForSeconds(attackCooldown);
        isOnCooldown = false;
    }

    public virtual void Attack() {}
}
