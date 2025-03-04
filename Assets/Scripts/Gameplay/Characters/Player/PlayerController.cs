// PlayerController.cs
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float movementLerp = 0.1f;

    [Header("References")]
    [SerializeField] private InputActionReference movement;
    [SerializeField] private InputActionReference attack;
    [SerializeField] private InputActionReference setElementalForward;
    [SerializeField] private InputActionReference setElementalBackward;
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

    public void OnEnable()
    {
        movement.action.Enable();
        attack.action.Enable();
        setElementalForward.action.Enable();
        setElementalBackward.action.Enable();
    }

    public void OnDisable()
    {
        movement.action.Disable();
        attack.action.Disable();
        setElementalForward.action.Disable();
        setElementalBackward.action.Disable();
    }

    //Move player
    public void SetMovement(bool state) => canMove = state;
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

    // Set Elemental Attacks
    public void CycleElementalAttackForward(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            int index = PlayerStats.Instance.currentElementalAttackIndex;
            PlayerStats.Instance.SetCurrentElemental((index + 1) % 4);
        }
    }

    public void CycleElementalAttackBackward(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            int index = PlayerStats.Instance.currentElementalAttackIndex;
            PlayerStats.Instance.SetCurrentElemental((index - 1 + 4) % 4);
        }
    }

    //Melee attack
    public void OnAttack(InputAction.CallbackContext ctx)
    {
        Debug.Log("Melee attack");
        if (PlayerMeleeCombat.Instance != null)
        {
            if (ctx.performed)
            {
                rb.velocity = Vector2.zero;
                PlayerMeleeCombat.Instance.AttemptAttack();
            }
        }
        else
        {
            Debug.Log("PlayerMeleeCombat.Instance is missing");
        }
    }

    // Ranged Attack
    public void OnRangedAttack(InputAction.CallbackContext ctx)
    {
        if (PlayerRangedCombat.Instance != null)
        {
            if (ctx.started)
            {
                Debug.Log("Aim Started");
                rb.velocity = Vector2.zero;
                PlayerRangedCombat.Instance.StartAim();
            }
            if (ctx.canceled)
            {
                Debug.Log("Aim Stopped and attack");
                PlayerRangedCombat.Instance.StopAimAndAttack();
            }
        }
    }

}