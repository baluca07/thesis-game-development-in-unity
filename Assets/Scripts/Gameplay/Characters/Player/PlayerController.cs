// PlayerController.cs
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float movementLerp = 0.1f;

    [Header("References")]
    [SerializeField] private InputActionReference movement;
    [SerializeField] private InputActionReference attack;
    [SerializeField] private Animator animator;

    private Rigidbody2D rb;
    private Vector2 currentVelocity;
    private bool canMove = true;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        rb = GetComponent<Rigidbody2D>();

        if (animator == null)
            animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (canMove)
        {
            UpdateAnimations();
            UpdateRotation();
        }
    }

    private void FixedUpdate() => Move();

    private void OnEnable()
    {
        //if (attack?.action != null)
        //    attack.action.performed += OnAttack;
        movement.action.Enable();
    }

    private void OnDisable()
    {
        //if (attack?.action != null)
        //    attack.action.performed -= OnAttack;
        movement.action.Disable();
    }

    public void SetMovement(bool state) => canMove = state;

    private void OnAttack(InputAction.CallbackContext ctx)
    {
        if (PlayerMeleeCombat.Instance != null)
            {
            rb.velocity = Vector2.zero;
            PlayerMeleeCombat.Instance.AttemptAttack();
        }
    }

    private void Move()
    {
        if (!canMove) return;

        Vector2 input = movement.action.ReadValue<Vector2>();
        Vector2 targetVelocity = input.normalized * moveSpeed;
        rb.velocity = Vector2.SmoothDamp(rb.velocity, targetVelocity, ref currentVelocity, movementLerp);
    }

    private void UpdateRotation()
    {
        if (!canMove) return;

        Vector2 input = movement.action.ReadValue<Vector2>();
        if (input.x != 0)
            transform.rotation = Quaternion.Euler(0, input.x > 0 ? 0 : 180, 0);
    }

    private void UpdateAnimations()
    {
        bool isMoving = canMove && movement?.action != null &&
                      movement.action.ReadValue<Vector2>().sqrMagnitude > 0.1f;
        animator.SetBool("Run", isMoving);
    }
}