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
    private bool isAttacking = false; // T�mad�s �llapot�t jelz� v�ltoz�

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
        if (!isAttacking) // Mozg�s csak akkor friss�l, ha nincs t�mad�s
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
        if (!isAttacking) // Csak akkor t�rt�nik mozg�s, ha nincs t�mad�s
        {
            rigidbodyPlayer.velocity = moveDirection.normalized * moveSpeed;
        }
        else
        {
            rigidbodyPlayer.velocity = Vector2.zero; // T�mad�s alatt meg�ll�tjuk a mozg�st
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            animator.SetTrigger("Attack"); // T�mad�s anim�ci� ind�t�sa
        }
    }

    /// <summary>
    /// Anim�ci�s esem�nyek �ltal megh�vott met�dusok
    /// </summary>
    public void DisableMovement() // T�mad�s elej�n mozg�s letilt�sa
    {
        isAttacking = true;
    }

    public void EnableMovement() // T�mad�s v�g�n mozg�s enged�lyez�se
    {
        isAttacking = false;
    }
}
