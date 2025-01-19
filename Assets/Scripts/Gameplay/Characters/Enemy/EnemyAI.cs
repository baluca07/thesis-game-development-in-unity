using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] Transform player;
    private EnemyStats enemy;

    private bool isAttacking = false;
    private bool canDash = true;

    [SerializeField] float attackRange = 1.2f; // Támadási távolság
    [SerializeField] float dashInterval = 2f; // Dash-ek közötti idõköz
    [SerializeField] float dashDuration = 0.5f; // A dash idõtartama
    [SerializeField] float dashSpeed = 5f; // Dash sebesség

    private Animator anim;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemy = GetComponent<EnemyStats>();
        anim = GetComponent<Animator>();
    }

    private void ChasePlayer()
    {
        if (Vector3.Distance(transform.position, player.position) > attackRange && !isAttacking)
        {
            if (canDash)
            {
                Vector3 directionToPlayer = (player.position - transform.position).normalized;
                Debug.Log("Dash started");
                StartCoroutine(Dash(directionToPlayer));
                anim.SetTrigger("Jump");
            }
        }
        else
        {
            isAttacking = true;
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
            TurnTowardsPlayer();
            ChasePlayer();
        }
    }

    private void TurnTowardsPlayer()
    {
        if (player.transform.position.x > transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
}
