using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Transform player;
    private EnemyStats enemy;

    private bool canDash = true;

    private bool takingDamage = false;

    //[SerializeField] float dashInterval = 2f;
    [SerializeField] float dashDuration = 1f;
    [SerializeField] float dashSpeed = 1f;

    private MeleeEnemyAttack enemyAttack;

    private Animator anim;

    [SerializeField] ParticleSystem particle;

    [SerializeField] Rigidbody2D rb;


    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        enemy = GetComponent<EnemyStats>();
        anim = GetComponent<Animator>();
        enemyAttack = GetComponent<MeleeEnemyAttack>();
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (player != null)
        {
            if (!PlayerInAttackRange() && canDash && !enemyAttack.isAttacking && !takingDamage)
            {
                anim.SetTrigger("Jump");
            }
            anim.SetBool("CanAttack", CanAttack());
        }
    }

    public void PlayJumpSound()
    {
        AudioController.Instance.PlayEnemyJumpSound(audioSource);
    }
    private bool PlayerInAttackRange()
    {
        return Vector3.Distance(transform.position, player.position) <= enemy.attackRange;
    }

    public void PlayParticles()
    {
        particle.Play();
    }

    //Used in animaton, started by animation event.

    private IEnumerator PushBackTimer()
    {
        canDash = false;
        takingDamage = true;
        float elapsedTime = 0f;

        Vector3 direction = (transform.position - PlayerStats.Instance.transform.position).normalized;

        while (elapsedTime < 0.5f)
        {
            transform.position += direction * 0.5f * Time.deltaTime;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        Debug.Log("Pushed back");
        canDash = true;
        takingDamage = false;
    }

    public void PushBack()
    {
        StartCoroutine(PushBackTimer());
    }

    private IEnumerator Dash(Vector3 direction)
    {
        canDash = false;
        float elapsedTime = 0f;

        while (elapsedTime < dashDuration && Vector3.Distance(transform.position, player.position) > enemy.attackRange && !takingDamage)
        {
            transform.position += direction * dashSpeed * Time.deltaTime;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Debug.Log("Dash ended");
        //yield return new WaitForSeconds(dashInterval);
        canDash = true;
    }

    private bool CanAttack()
    {
        return PlayerInAttackRange() && (!takingDamage) ;
    }

    public void Move()
    {
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        StartCoroutine(Dash(directionToPlayer));
    }

    public void TurnTowardsPlayer()
    {
        if (PlayerStats.Instance.transform.position.x > transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    public void StopSliding()
    {
        rb.velocity = Vector3.zero;
    }
}
