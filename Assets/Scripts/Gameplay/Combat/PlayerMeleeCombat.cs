using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMeleeCombat : MonoBehaviour
{
    public static PlayerMeleeCombat Instance;

    [SerializeField] CircleCollider2D meleeCollider;
    private HashSet<EnemyStats> hitEnemies = new HashSet<EnemyStats>();

    [SerializeField] Animator animator;

    public float damageSpeed = 0.4f;

    protected bool isOnCooldown = false;

    public int maxAttackCounter = 3;
    private int currentAttackCounter = 0;
    public Coroutine attackResetCoroutine;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

    }

    private void Start()
    {
        meleeCollider = GetComponent<CircleCollider2D>();
        meleeCollider.enabled = false;
        animator = GetComponent<Animator>();
    }

    public IEnumerator Attack()
    {
        float moveDistance = 0.2f;
        transform.Translate(Vector3.right * moveDistance);

        meleeCollider.enabled = true;
        IncreaseAttackCounter();

        yield return new WaitForSeconds(damageSpeed);

        meleeCollider.enabled = false;
        hitEnemies.Clear();
    }

    private void IncreaseAttackCounter()
    {
        if (currentAttackCounter < maxAttackCounter)
        {
            currentAttackCounter++;
            animator.SetInteger("AttackCounter", currentAttackCounter);
        }
        else
        {
            currentAttackCounter = 1;
            animator.SetInteger("AttackCounter", currentAttackCounter);
        }
    }

    public IEnumerator ResetAttackCounterAfterDelay()
    {
        yield return new WaitForSeconds(damageSpeed);

        if (currentAttackCounter > 0)
        {
            currentAttackCounter = 0;
            animator.SetInteger("AttackCounter", currentAttackCounter);
            Debug.Log("Attack counter reset due to inactivity.");
        }
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
                enemy.TakeDamage(PlayerStats.Instance.currentElementalAttack.GetDamage());
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
