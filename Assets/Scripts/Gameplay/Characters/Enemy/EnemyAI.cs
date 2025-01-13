using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] GameObject player;
    private EnemyStats enemy;

    private EnemyAttack enemyAttack;

    private float lastAttackTime = 0f;
    
    private bool isAttacking = false;


    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemy = GetComponent<EnemyStats>();
        enemyAttack = GetComponent<EnemyAttack>();
    }

    private void ChasePlayer()
    {
        if (Vector3.Distance(transform.position, player.transform.position) > enemy.attackRange)
        {
            Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;
            transform.position += directionToPlayer * enemy.speed * Time.deltaTime;
        }
        else
        {
            isAttacking = true;
        }
    }

    private void Update()
    {

        if (player != null)
        {
            if (!isAttacking)
            {
                ChasePlayer();
            }

            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            if (distanceToPlayer <= enemy.attackRange)
            {
                if (Time.time >= lastAttackTime + enemy.attackCoolDown)
                {
                    if (enemyAttack != null)
                    {
                        enemyAttack.Attack(player.GetComponent<PlayerStats>());
                        lastAttackTime = Time.time;
                    }
                    else
                    {
                        Debug.LogError("No attack component available to perform the attack!");
                    }
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, player.transform.position) > enemy.attackRange)
        {
            isAttacking = false;
        }
    }
}

