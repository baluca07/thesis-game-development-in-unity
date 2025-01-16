using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MeleeWeapon : Weapon
{
    //[Header("Need to add DamageZone")]
    //[SerializeField] GameObject damageZonePrefab;
    public float meleeRange;

    [SerializeField] GameObject player;
    [SerializeField] CircleCollider2D meleeCollider;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Color originalColor;

    private HashSet<EnemyStats> hitEnemies = new HashSet<EnemyStats>();

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        meleeCollider = player.GetComponent<CircleCollider2D>();
        meleeCollider.enabled = false;
        meleeCollider.radius = meleeRange;
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        spriteRenderer.color = Color.clear;
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
        spriteRenderer.color = originalColor;
        meleeCollider.enabled = true;

        yield return new WaitForSeconds(damageSpeed);

        meleeCollider.enabled = false;
        hitEnemies.Clear();
        spriteRenderer.color = Color.clear;

    }
}
