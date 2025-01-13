using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RangedWeapon : MonoBehaviour
{
    [Header("Manualy Managed")]
    [Header("Basic Stats")]
    public string weaponName;
    public int baseDamage;
    public float attackCooldown;

    [Header("Projectile")]
    public float projectileSpeed;
    public float projectileRange;
    [SerializeField] GameObject projectilePrefab;

    private bool isOnCooldown = false;

    [Header("Damage Category")]
    public DamageCategory damageCategory;

    [Header("Damage Type")]
    public ElementalDamageType elementalDamageType;

    [Header("Need to add Sprite")]
    [SerializeField] SpriteRenderer sprite;

    private Color originalSpriteColor;

    public Damage damage;

    private void Start()
    {
        damage = new Damage(damageCategory, elementalDamageType, baseDamage);
        sprite = GetComponent<SpriteRenderer>();
        sprite.enabled = true;
        originalSpriteColor = sprite.color;
        //sprite.color = Color.clear;
    }

    public void PerformRangedAttack(ref bool isAttack)
    {
        if (!isOnCooldown)
        {
            isAttack = true;
            Debug.Log("Attack performed");
            ShootProjectile();
            isAttack = false;
            StartCoroutine(AttackCoolDown());
        }
        else
        {
            Debug.Log($"{weaponName} is on cooldown");
            //Cooldown logic comes here
        }
    }

    private void ShootProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
        Projectile projectileScript = projectile.GetComponent<Projectile>();

        if (projectileScript != null)
        {
            projectileScript.damage = damage;
            projectileScript.attackRange = projectileRange;
            projectileScript.speed = projectileSpeed;
            projectileScript.owner = Projectile.ProjectileOwner.Enemy;
        }
        Debug.Log("Shoot projectile.");
    }

    private IEnumerator AttackCoolDown()
    {
        isOnCooldown = true;
        yield return new WaitForSeconds(attackCooldown);
        isOnCooldown = false;
    }

}
