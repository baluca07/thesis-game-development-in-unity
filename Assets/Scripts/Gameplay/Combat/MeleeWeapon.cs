using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class MeleeWeapon : BaseMeleeAttack
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

    private HashSet<Enemy> hitEnemies = new HashSet<Enemy>();

    public Damage damage;

    private void Start()
    {
        damage = new Damage(damageCategory, elementalDamageType, baseDamage);
        meleeCollider2D = GetComponent<Collider2D>();
    }
    public void PerformMeleeAttack(ref bool isMeleeAttack, Vector2 attackDirection)
    {
        hitEnemies.Clear();
        isMeleeAttack = true;
        Debug.Log("Attack performed");
        StartCoroutine(PerformColliderChange(meleeCollider2D, attackDirection, damageSpeed));
        isMeleeAttack = false;
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
