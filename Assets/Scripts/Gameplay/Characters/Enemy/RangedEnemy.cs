using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float attackCooldown = 2f; // Time between attacks
    public float projectileSpeed = 10f;

    private float lastAttackTime;

    /*public override void Attack(GameObject target)
    {
        if (Time.time - lastAttackTime >= attackCooldown)
        {
            lastAttackTime = Time.time;

            // Create and launch the projectile
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
            Vector3 direction = (target.transform.position - firePoint.position).normalized;

            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = direction * projectileSpeed;
            }

            Debug.Log($"{enemyData.enemyName} performs a ranged attack on {target.name}!");
        }
    }*/
}

