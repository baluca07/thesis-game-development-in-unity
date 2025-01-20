using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private Transform player;
    private EnemyStats enemy;

    private bool canAttack = false;
    private bool canDash = true;

    [SerializeField] float attackRange = 1.2f;
    [SerializeField] float dashInterval = 2f;
    [SerializeField] float dashDuration = 1f;
    [SerializeField] float dashSpeed = 1f;

    private MeleeEnemyAttack enemyAttack;

    private Animator anim;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemy = GetComponent<EnemyStats>();
        anim = GetComponent<Animator>();
        enemyAttack = GetComponent<MeleeEnemyAttack>();
    }

    private void ChasePlayer()
    {
        if (!PlayerInAttackRange() && canDash)
        {
            anim.SetTrigger("Jump");
            TurnTowardsPlayer(this.gameObject);
            Vector3 directionToPlayer = (player.position - transform.position).normalized;
            Debug.Log("Dash started");
            StartCoroutine(Dash(directionToPlayer));
        }
        else if (PlayerInAttackRange())
        {
            anim.SetTrigger("Attack");
        }
    }

    private IEnumerator Dash(Vector3 direction)
    {
        canDash = false;
        float elapsedTime = 0f;

        while (elapsedTime < dashDuration && Vector3.Distance(transform.position, player.position) > attackRange)
        {
            transform.position += direction * dashSpeed * Time.deltaTime;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Debug.Log("Dash ended");
        yield return new WaitForSeconds(dashInterval);
        canDash = true;
    }

    private void Update()
    {
        if (player != null)
        {
            ChasePlayer();
        }
    }

    private bool PlayerInAttackRange()
    {
        return Vector3.Distance(transform.position, player.position) <= attackRange;
    }

    public static void TurnTowardsPlayer(GameObject obj)
    {
        if (PlayerStats.Instance.transform.position.x > obj.transform.position.x)
        {
            obj.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            obj.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
}
