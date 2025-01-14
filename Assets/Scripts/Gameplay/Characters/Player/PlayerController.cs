using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControll : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] Rigidbody2D rigidbodyPlayer;
    [SerializeField] Animator animatior;

    [Header("Movement")]
    public float moveSpeed = 5f;
    public InputActionReference move;

    private Vector2 moveDirection;


    private void Awake()
    {
        rigidbodyPlayer = GetComponent<Rigidbody2D>();
        animatior = GetComponent<Animator>();

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
        moveDirection = move.action.ReadValue<Vector2>();
        if (moveDirection.x > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (moveDirection.x < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        if (moveDirection != Vector2.zero)
        {
            animatior.SetBool("Run", true);
        }
        else
        {
            animatior.SetBool("Run", false);
        }
    }

    private void FixedUpdate()
    {
        rigidbodyPlayer.velocity = moveDirection.normalized * moveSpeed;
    }
}
