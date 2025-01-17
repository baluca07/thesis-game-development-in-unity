using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Rigidbody2D rigidbodyPlayer;
    [SerializeField] private Animator animator;

    [Header("Movement")]
    public float moveSpeed = 5f;
    public InputActionReference move;

    private Vector2 moveDirection;
    private bool isAttacking; // Támadás állapotát jelzõ változó

    private void Awake()
    {
        rigidbodyPlayer = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        move.action.Enable();
    }

    private void OnDisable()
    {
        move.action.Disable();
    }

    private void Update()
    {
        // Csak akkor frissítjük a mozgást, ha nincs támadás
            moveDirection = move.action.ReadValue<Vector2>();

            if (moveDirection != Vector2.zero)
            {
                animator.SetBool("Run", true);
            }
            else
            {
                animator.SetBool("Run", false);
            }

            if (moveDirection.x > 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else if (moveDirection.x < 0)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
    }

    private void FixedUpdate()
    {
        // Mozgás csak akkor történik, ha nincs támadás
        if (!isAttacking)
        {
            rigidbodyPlayer.velocity = moveDirection.normalized * moveSpeed;
        }
        else
        {
            rigidbodyPlayer.velocity = Vector2.zero; // Támadás alatt azonnal megállítjuk a mozgást
        }
    }

    public void OnSwitchToPreviousElement(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            int index = PlayerStats.Instance.currentElementalAttackIndex;
            if (GameManager.Instance.elementalAttacks[index].CanActivateElemental())
            {
                index--;
                if (index < 0)
                {
                    index = 4;
                }
                PlayerStats.Instance.SetCurrentElemental(index);
                UIManager.Instance.UpdateElementalType();
            }
        }
    }

    public void OnSwitchToNextElement(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            int index = PlayerStats.Instance.currentElementalAttackIndex;
            if (GameManager.Instance.elementalAttacks[index].CanActivateElemental())
            {
                index++;
                if (index >= 5)
                {
                    index = 0;
                }
                PlayerStats.Instance.SetCurrentElemental(index);
                UIManager.Instance.UpdateElementalType();
            }
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isAttacking = true; // Támadás elkezdésekor megszakítjuk a mozgást

            StartCoroutine(PlayerMeleeCombat.Instance.Attack());

            if (PlayerMeleeCombat.Instance.attackResetCoroutine != null)
            {
                StopCoroutine(PlayerMeleeCombat.Instance.attackResetCoroutine);
            }
            PlayerMeleeCombat.Instance.attackResetCoroutine = StartCoroutine(PlayerMeleeCombat.Instance.ResetAttackCounterAfterDelay());

            // Támadás végén visszaállítjuk a mozgást
            StartCoroutine(ResetAttackStateAfterDelay(PlayerMeleeCombat.Instance.damageSpeed));
        }
    }

    private IEnumerator ResetAttackStateAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        isAttacking = false; // Támadás végeztével engedélyezzük a mozgást
    }
}
