using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MeleeAttack : MonoBehaviour
{
    private bool isMeleeAttack = false;
    public Collider2D meleeAttackRange;

    void Update()
    {
        if (!isMeleeAttack) return;

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        Vector2 direction = (mousePosition - transform.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        Debug.DrawLine(transform.position, mousePosition, Color.blue);
    }

    public void OnMeleeAttack(InputAction.CallbackContext context)
    {
        // A Tap interakció esetében a `performed` eseményt használd
        if (context.performed)
        {
            StartCoroutine(PerformMeleeAttack());
        }
    }

    private IEnumerator PerformMeleeAttack()
    {
        isMeleeAttack = true;
        ActivateAttackRange();

        // Az animáció vagy támadás ideje alatt várakozás
        yield return new WaitForSeconds(1f); // Példa: 0.2 másodperc, állítsd be a támadás idejére

        isMeleeAttack = false;
        DeactivateAttackRange();
    }

    private void ActivateAttackRange()
    {
        if (meleeAttackRange != null)
        {
            meleeAttackRange.enabled = true;
            Debug.Log("Activate collider");
        }
        else
        {
            Debug.LogWarning("Melee Attack Range Collider is not assigned!");
        }
    }

    private void DeactivateAttackRange()
    {
        if (meleeAttackRange != null)
        {
            meleeAttackRange.enabled = false;

            Debug.Log("Deactivate collider");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isMeleeAttack) return;

        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Enemy hit: " + other.name);
            // Damage Enemy
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!isMeleeAttack) return;

        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Enemy left attack range: " + other.name);
        }
    }
}
