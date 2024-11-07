using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;

    private Vector2 movementInput;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
        Debug.Log(movementInput);
        Debug.Log("Player moves");
    }
    void FixedUpdate()
    {

        Vector2 move = movementInput * speed * Time.deltaTime;
        rb.velocity = move;
    }
}