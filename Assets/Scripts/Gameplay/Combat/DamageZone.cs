using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{

    public Damage damage;

    private HashSet<EnemyStats> hitEnemies = new HashSet<EnemyStats>();

    private void Start()
    {
        if (GetComponent<Collider2D>() != null)
        {
            Collider2D collider = GetComponent<Collider2D>();
            collider.isTrigger = true;
        }
        Debug.Log(damage.amount);
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
