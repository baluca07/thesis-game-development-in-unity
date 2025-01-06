using System.Collections;
using UnityEngine;

public class BaseAttack : MonoBehaviour
{
    public void SetAttackDirection(Collider2D attackCollider, Vector2 direction)
    {
       
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            attackCollider.transform.rotation = Quaternion.Euler(0, 0, angle);
    
    }
    public void ActivateAttackRange(Collider2D attackCollider, Vector2 direction)
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

    public void DeactivateAttackRange(Collider2D attackCollider)
    {
            attackCollider.enabled = false;
    }

    public IEnumerator PerformMeleeAttack(Collider2D attackCollider, Vector2 direction, float attackTime)
    {
        ActivateAttackRange(attackCollider,direction);

        yield return new WaitForSeconds(attackTime);

        DeactivateAttackRange(attackCollider);
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
    }
}

