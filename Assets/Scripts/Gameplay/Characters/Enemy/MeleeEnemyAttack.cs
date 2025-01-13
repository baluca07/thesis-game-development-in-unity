using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemyAttack : EnemyAttack 
{
    private EnemyStats stats;
    private Damage damage;

    private void Start()
    {
        stats = GetComponent<EnemyStats>();
        damage = new Damage(stats.damageCategory, stats.elementalDamageType, stats.baseDamage);
    }

   public override void Attack(PlayerStats player)
    {
        Debug.Log($"{stats.enemyName} performs a melee attack on {player.name} causing {stats.baseDamage} damage!");
        player.TakeDamage(damage);
    }

    private void Update()
    {
        DrawArc(transform.position, stats.attackRange);
    }

    private void DrawArc(Vector3 center, float radius)
    {
        float angleStep = 360f / 36; // Sz�g minden egyes vonal k�z�tt
        float currentAngle = 0f;

        for (int i = 0; i < 36; i++)
        {
            // Jelenlegi pont kisz�m�t�sa
            Vector3 startPoint = center + new Vector3(
                Mathf.Cos(currentAngle * Mathf.Deg2Rad) * radius,
                Mathf.Sin(currentAngle * Mathf.Deg2Rad) * radius,
                0f
            );

            // K�vetkez� pont kisz�m�t�sa
            currentAngle += angleStep;
            Vector3 endPoint = center + new Vector3(
                Mathf.Cos(currentAngle * Mathf.Deg2Rad) * radius,
                Mathf.Sin(currentAngle * Mathf.Deg2Rad) * radius,
                0f
            );

            // Vonal rajzol�sa
            Debug.DrawLine(startPoint, endPoint, Color.blue);
        }
    }
}

    