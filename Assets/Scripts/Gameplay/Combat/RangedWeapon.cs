using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : MonoBehaviour
{
    [Header("Manualy Managed")]
    [Header("Basic Stats")]
    public string weaponName;
    public int baseDamage;
    public float damageSpeed;
    public float attackCooldown;

    [SerializeField] GameObject projectile;

    private bool isOnCooldown = false;

    [Header("Damage Category")]
    public DamageCategory damageCategory;

    [Header("Damage Type")]
    public ElementalDamageType elementalDamageType;

    [Header("Need to add DamageZone")]
    [SerializeField] GameObject damageZonePrefab;

    [Header("Need to add Sprite")]
    [SerializeField] SpriteRenderer sprite;

    private Color originalSpriteColor;

    public Damage damage;
}
