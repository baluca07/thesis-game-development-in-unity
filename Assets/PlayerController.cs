using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControll : MonoBehaviour
{
    [Header("Components")]
    private Rigidbody2D rb;

    [Header("Movement")]
    public float moveSpeed = 5f;
    public InputActionReference move;

    private Vector2 _moveDirection;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
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
        _moveDirection = move.action.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        rb.velocity = _moveDirection.normalized * moveSpeed;
    }
}
