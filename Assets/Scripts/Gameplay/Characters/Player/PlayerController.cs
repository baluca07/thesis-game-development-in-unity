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
    private bool isAttacking = false; // Támadás állapotát jelzõ változó

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
        if (!isAttacking) // Mozgás csak akkor frissül, ha nincs támadás
        {
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
    }

    private void FixedUpdate()
    {
        if (!isAttacking) // Csak akkor történik mozgás, ha nincs támadás
        {
            rigidbodyPlayer.velocity = moveDirection.normalized * moveSpeed;
        }
        else
        {
            rigidbodyPlayer.velocity = Vector2.zero; // Támadás alatt megállítjuk a mozgást
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            animator.SetTrigger("Attack"); // Támadás animáció indítása
        }
    }

    /// <summary>
    /// Animációs események által meghívott metódusok
    /// </summary>
    public void DisableMovement() // Támadás elején mozgás letiltása
    {
        isAttacking = true;
    }

    public void EnableMovement() // Támadás végén mozgás engedélyezése
    {
        isAttacking = false;
    }
}
