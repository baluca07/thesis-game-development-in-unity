using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{
    public float attackRange = 1.5f; // Melee attack range
    [SerializeField] CircleCollider2D attackCollider;

    private Damage damage;

    //Delete when add enemyAI
    public PlayerStats player;
    
    private void Start()
    {
        damage = new Damage(enemyData.damageCategory, enemyData.elementalDamageType, enemyData.baseDamage);

        attackCollider = GetComponent<CircleCollider2D>();
        attackCollider.isTrigger = true;
        attackCollider.radius = attackRange;
    }

    public override void Attack(PlayerStats player)
    {
        Debug.Log($"{enemyData.enemyName} performs a melee attack on {player.name} causing {enemyData.baseDamage} damage!");
        player.TakeDamage(damage);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            Attack(player);
        }
    }
}

