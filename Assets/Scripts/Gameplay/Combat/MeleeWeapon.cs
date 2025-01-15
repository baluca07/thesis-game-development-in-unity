using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : Weapon
{
    [Header("Need to add DamageZone")]
    //[SerializeField] GameObject damageZonePrefab;
    [SerializeField] Collider2D attackCollider;

    [SerializeField] GameObject player;

    private HashSet<EnemyStats> hitEnemies = new HashSet<EnemyStats>();

    private void Start()
    {
        damage = new Damage(damageCategory, elementalDamageType, baseDamage);
        player = GameObject.FindGameObjectWithTag("Player");
        attackCollider = GetComponent<Collider2D>();

    }
    public override void Attack()
    {
        if(!isOnCooldown)
        {
            Debug.Log("Melee attack performed");
            StartCoroutine(SetDamageZone(damageSpeed));
            StartCoroutine(AttackCoolDown());
        }
        else
        {
            Debug.Log($"{weaponName} is on cooldown");
            //Cooldown logic comes here
        }
    }


    private IEnumerator SetDamageZone(float damageSpeed)
    {
        /* if (damageZonePrefab != null)
         {
             Vector3 spawnPosition = transform.position + damageZonePrefab.transform.localPosition;
             float characterFacingYRotation = player.transform.rotation.eulerAngles.y;
             if (characterFacingYRotation == -180)
             {
                 spawnPosition.x = Math.Abs(spawnPosition.x);
             }


             // Instantiate the damage zone
             GameObject damageZone = Instantiate(damageZonePrefab, spawnPosition, transform.rotation);

             DamageZone damageZoneScript = damageZone.GetComponent<DamageZone>();

             if (damageZoneScript != null)
             {
                 damageZoneScript.damage = damage;  // Passing the damage
             }

             yield return new WaitForSeconds(damageSpeed);

             Destroy(damageZone);

         }
         else
         {
             Debug.LogWarning("Damage Zone is not assigned!");
         }*/
        attackCollider.enabled = true;

        yield return new WaitForSeconds(damageSpeed);

        attackCollider.enabled = false;
        hitEnemies.Clear();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"Object triggered: {collision.name}");

        if (collision.CompareTag("Enemy"))
        {
            EnemyStats enemy = collision.GetComponent<EnemyStats>();
            Debug.Log($"Enemy collided: {enemy.enemyName}");
            if (!hitEnemies.Contains(enemy))
            {
                hitEnemies.Add(enemy);
                Debug.Log($"Enemy added to hitEnemies: {enemy.enemyName}");
                enemy.TakeDamage(damage);
                Debug.Log($"Enemy damaged: {enemy.enemyName}");
            }
            else
            {
                Debug.Log($"Enemy is already hit by player: {enemy.enemyName}");
            }

            Debug.Log(hitEnemies.Count);
        }
    }
}
