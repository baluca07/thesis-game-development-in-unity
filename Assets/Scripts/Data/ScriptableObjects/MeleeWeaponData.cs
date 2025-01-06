using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMeleeWeapon", menuName = "Game/Weapon/Melee")]
public class MeleeWeaponData : ScriptableObject
{
    [Header("Basic Stats")]
    public string weaponName;
    public int baseDamage;
    public int damageSpeed;

    [Header("Damage Category")]
    public DamageCategory damageCategory;

    [Header("Damage Type")]
    public ElementalDamageType elementalDamageType;

}
