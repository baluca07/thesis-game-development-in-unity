using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMeleeCombat : MonoBehaviour
{
    public static PlayerMeleeCombat Instance;

    [Header("Combat Settings")]
    [SerializeField] private float comboWindow = 0.5f;
    [SerializeField] private float attackDuration = 0.4f;
    [SerializeField] private float lungeDistance = 0.2f;

    [Header("References")]
    [SerializeField] private CircleCollider2D meleeCollider;
    [SerializeField] private Animator animator;

    private readonly HashSet<EnemyStats> hitEnemies = new HashSet<EnemyStats>();
    private Coroutine comboResetRoutine;
    private int currentCombo;
    private bool isAttacking;

    private void Awake() => Instance = this;

    public void AttemptAttack()
    {
        if (isAttacking) return;

        StartCoroutine(AttackRoutine());
        ResetComboTimer();
    }

    private IEnumerator AttackRoutine()
    {
        isAttacking = true;
        PlayerController.Instance.SetMovement(false);

        // Update combo state (1-2-3 cycle)
        currentCombo = currentCombo < 3 ? currentCombo + 1 : 1;
        animator.SetInteger("AttackCounter", currentCombo);

        // Apply lunge effect using position translation
        ApplyAttackLunge();

        meleeCollider.enabled = true;
        yield return new WaitForSeconds(attackDuration);
        meleeCollider.enabled = false;

        hitEnemies.Clear();
        isAttacking = false;
    }

    private void ResetComboTimer()
    {
        if (comboResetRoutine != null) StopCoroutine(comboResetRoutine);
        comboResetRoutine = StartCoroutine(ComboResetCountdown());
    }

    private IEnumerator ComboResetCountdown()
    {
        yield return new WaitForSeconds(comboWindow);
        currentCombo = 0;
        animator.SetInteger("AttackCounter", 0);
        PlayerController.Instance.SetMovement(true);
    }

    private void ApplyAttackLunge()
    {
        // Use direct position translation instead of physics forces
        Vector2 lungeDirection = transform.right * lungeDistance;
        transform.Translate(lungeDirection, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Enemy")) return;

        if (other.TryGetComponent<EnemyStats>(out var enemy) && !hitEnemies.Contains(enemy))
        {
            hitEnemies.Add(enemy);
            enemy.TakeDamage(PlayerStats.Instance.currentElementalAttack.GetDamage());
        }
    }
}