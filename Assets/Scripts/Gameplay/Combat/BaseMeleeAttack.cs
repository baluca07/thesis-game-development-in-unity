using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMeleeAttack : MonoBehaviour
{
    private void SetAttackDirection(Collider2D attackCollider, Vector2 direction)
    {
       
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            attackCollider.transform.rotation = Quaternion.Euler(0, 0, angle);
    
    }
    private void ActivateAttackRange(Collider2D attackCollider, Vector2 direction)
    {
        if (attackCollider != null)
        {
            SetAttackDirection(attackCollider, direction);
            attackCollider.enabled = true;
        }
        else
        {
            Debug.LogWarning("Attack Collider is not assigned!");
        }
   }

    private void DeactivateAttackRange(Collider2D attackCollider)
    {
            attackCollider.enabled = false;
    }

    public IEnumerator PerformColliderChange(Collider2D attackCollider, Vector2 direction, float damageSpeed)
    {
        ActivateAttackRange(attackCollider,direction);

        yield return new WaitForSeconds(damageSpeed);

        DeactivateAttackRange(attackCollider);
    }
}

