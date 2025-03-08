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
    [SerializeField] private Animator anim;

    private readonly HashSet<EnemyStats> hitEnemies = new HashSet<EnemyStats>();
    private Coroutine comboResetRoutine;
    private int currentCombo;
    private bool isAttacking;

    private void Awake() => Instance = this;

    private void Start()
    {
        anim = GetComponent<Animator>();
        meleeCollider = GetComponent<CircleCollider2D>();

    }

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
        anim.SetInteger("AttackCounter", currentCombo);

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
        anim.SetInteger("AttackCounter", 0);
        PlayerController.Instance.SetMovement(true);
    }

    private void ApplyAttackLunge()
    {
        // Use direct position translation instead of physics forces
        Vector2 lungeDirection = transform.right * lungeDistance;
        transform.Translate(lungeDirection, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy")) return;

        EnemyStats enemy = collision.GetComponent<EnemyStats>();

        if (!hitEnemies.Contains(enemy) && collision is PolygonCollider2D)
        {
            Debug.Log("New enemy detected");
            hitEnemies.Add(enemy);
            enemy.TakeDamage(GameManager.Instance.normalAttack.GetDamage());
        }
    }
    public void EnableAttackCollider() { meleeCollider.enabled = true; }
    public void DisableAttackCollider() { meleeCollider.enabled = false; }


}