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
    private bool isAttacking; // T�mad�s �llapot�t jelz� v�ltoz�

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
        // Csak akkor friss�tj�k a mozg�st, ha nincs t�mad�s
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
        // Mozg�s csak akkor t�rt�nik, ha nincs t�mad�s
        if (!isAttacking)
        {
            rigidbodyPlayer.velocity = moveDirection.normalized * moveSpeed;
        }
        else
        {
            rigidbodyPlayer.velocity = Vector2.zero; // T�mad�s alatt azonnal meg�ll�tjuk a mozg�st
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
            isAttacking = true; // T�mad�s elkezd�sekor megszak�tjuk a mozg�st

            StartCoroutine(PlayerMeleeCombat.Instance.Attack());

            if (PlayerMeleeCombat.Instance.attackResetCoroutine != null)
            {
                StopCoroutine(PlayerMeleeCombat.Instance.attackResetCoroutine);
            }
            PlayerMeleeCombat.Instance.attackResetCoroutine = StartCoroutine(PlayerMeleeCombat.Instance.ResetAttackCounterAfterDelay());

            // T�mad�s v�g�n vissza�ll�tjuk a mozg�st
            StartCoroutine(ResetAttackStateAfterDelay(PlayerMeleeCombat.Instance.damageSpeed));
        }
    }

    private IEnumerator ResetAttackStateAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        isAttacking = false; // T�mad�s v�gezt�vel enged�lyezz�k a mozg�st
    }
}
