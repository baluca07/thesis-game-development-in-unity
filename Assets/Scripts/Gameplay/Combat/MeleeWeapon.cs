using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{

    [Header("Basic Stats")]
    public string weaponName;
    public int baseDamage;
    public float damageSpeed;
    public float attackCoolDown;

    [Header("Damage Category")]
    public DamageCategory damageCategory;

    [Header("Damage Type")]
    public ElementalDamageType elementalDamageType;

    [SerializeField] Collider2D meleeCollider2D;
    [SerializeField] SpriteRenderer sprite;
    private Color originalSpriteColor;

    private HashSet<Enemy> hitEnemies = new HashSet<Enemy>();

    public Damage damage;

    private void Start()
    {
        damage = new Damage(damageCategory, elementalDamageType, baseDamage);
        meleeCollider2D = GetComponent<Collider2D>();
        meleeCollider2D.enabled = false;
        sprite = GetComponent<SpriteRenderer>();
        sprite.enabled = true;
        originalSpriteColor = sprite.color;
        sprite.color = Color.clear;
    }
    public void PerformMeleeAttack(ref bool isMeleeAttack)
    {
        hitEnemies.Clear();
        isMeleeAttack = true;
        Debug.Log("Attack performed");
        StartCoroutine(PerformColliderChange(damageSpeed));
        isMeleeAttack = false;
    }

    private IEnumerator PerformColliderChange(float damageSpeed)
    {
        if (meleeCollider2D != null)
        {
            meleeCollider2D.enabled = true;
            sprite.color = originalSpriteColor;
        }
        else
        {
            Debug.LogWarning("Attack Collider is not assigned!");
        }

        yield return new WaitForSeconds(damageSpeed);

        meleeCollider2D.enabled = false;

        sprite.color = Color.clear;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log($"Object triggered: {collision.name}");

        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            //Debug.Log($"Enemy collided: {enemy.enemyData.enemyName}");
            if (!hitEnemies.Contains(enemy))
            {
                hitEnemies.Add(enemy);
                //Debug.Log($"Enemy added to hitEnemies: {enemy.enemyData.enemyName}");
                enemy.TakeDamage(damage);
                //Debug.Log($"Enemy damaged: {enemy.enemyData.enemyName}");


            }
            /*else {
                Debug.Log($"Enemy is already hit by player: {enemy.enemyData.enemyName}");
            }*/

            //Debug.Log(hitEnemies.Count);
        }
    }
}
