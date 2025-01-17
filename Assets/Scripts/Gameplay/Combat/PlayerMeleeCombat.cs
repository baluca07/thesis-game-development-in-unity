using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMeleeCombat : MonoBehaviour
{
    [SerializeField] CircleCollider2D meleeCollider;
    private HashSet<EnemyStats> hitEnemies = new HashSet<EnemyStats>();

    [SerializeField] Animator animatior;

    public float damageSpeed = 0.4f;

    protected bool isOnCooldown = false;

    public int maxAttackCounter = 3;
    private int currentAttackCounter = 0;
    private Coroutine attackResetCoroutine;

    private void Start()
    {
        meleeCollider = GetComponent<CircleCollider2D>();
        meleeCollider.enabled = false;
        animatior = GetComponent<Animator>();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            StartCoroutine(Attack());

            // Reset the attack counter if needed
            if (attackResetCoroutine != null)
            {
                StopCoroutine(attackResetCoroutine);
            }
            attackResetCoroutine = StartCoroutine(ResetAttackCounterAfterDelay());
        }
    }

    private IEnumerator Attack()
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
            animatior.SetInteger("AttackCounter", currentAttackCounter);
        }
        else
        {
            currentAttackCounter = 1;
            animatior.SetInteger("AttackCounter", currentAttackCounter);
        }
    }

    private IEnumerator ResetAttackCounterAfterDelay()
    {
        yield return new WaitForSeconds(damageSpeed);

        if (currentAttackCounter > 0)
        {
            currentAttackCounter = 0;
            animatior.SetInteger("AttackCounter", currentAttackCounter);
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
