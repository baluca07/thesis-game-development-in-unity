using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{

    public Damage damage;

    private HashSet<Enemy> hitEnemies = new HashSet<Enemy>();

    private void Start()
    {
        damage = new Damage(DamageCategory.Physical, ElementalDamageType.Normal, 10);
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
            Enemy enemy = collision.GetComponent<Enemy>();
            Debug.Log($"Enemy collided: {enemy.enemyData.enemyName}");
            if (!hitEnemies.Contains(enemy))
            {
                hitEnemies.Add(enemy);
                Debug.Log($"Enemy added to hitEnemies: {enemy.enemyData.enemyName}");
                enemy.TakeDamage(damage);
                Debug.Log($"Enemy damaged: {enemy.enemyData.enemyName}");
            }
            else
            {
                Debug.Log($"Enemy is already hit by player: {enemy.enemyData.enemyName}");
            }

            Debug.Log(hitEnemies.Count);
        }
    }
}
