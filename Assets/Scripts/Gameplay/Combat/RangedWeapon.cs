using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RangedWeapon : Weapon
{
    [Header("Projectile")]
    public float projectileSpeed;
    public float projectileRange;
    [SerializeField] GameObject projectilePrefab;

    public override void Attack()
    {
        if (!isOnCooldown)
        {
            Debug.Log("Attack performed");
            ShootProjectile();
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
            //projectileScript.damage = damage;
            projectileScript.attackRange = projectileRange;
            projectileScript.speed = projectileSpeed;
            projectileScript.owner = Projectile.ProjectileOwner.Enemy;
        }
        Debug.Log("Shoot projectile.");
    }

}
