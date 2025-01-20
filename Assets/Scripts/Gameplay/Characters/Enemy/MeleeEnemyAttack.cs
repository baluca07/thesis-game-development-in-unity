using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MeleeEnemyAttack : EnemyAttack
{
    private EnemyStats stats;
    private Damage damage;

    private Transform player;
    [SerializeField] float attackMoveDistance = 1f;
    [SerializeField] float attackMoveDuration = 1f;

    private CircleCollider2D circleCollider;

    private bool isAttacking = false;

    private void Start()
    {
        stats = GetComponent<EnemyStats>();
        player = PlayerStats.Instance.transform;
        damage = new Damage(stats.elementalDamageType, stats.baseDamage);
        circleCollider = GetComponent<CircleCollider2D>();
    }
    private void Update()
    {
        circleCollider.enabled = isAttacking;
    }

    public override void Attack()
    {
        if (!isAttacking)
        {
            StartCoroutine(PerformAttack());
        }
    }

    private IEnumerator PerformAttack()
    {
        isAttacking = true;
        EnemyAI.TurnTowardsPlayer(this.gameObject);

        Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;

        float elapsedTime = 0f;

        while (elapsedTime < attackMoveDuration)
        {
            transform.position += directionToPlayer * (attackMoveDistance / attackMoveDuration) * Time.deltaTime;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        isAttacking = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"Object triggered: {collision.name}");

        if (collision.CompareTag("Player"))
        {
            PlayerStats.Instance.TakeDamage(damage);
        }
    }
}
