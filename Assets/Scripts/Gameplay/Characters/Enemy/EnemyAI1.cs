using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDash : MonoBehaviour
{
    public Transform player;
    public float dashForce = 500f;
    public float dashInterval = 3f;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = PlayerStats.Instance.transform;
        StartCoroutine(DashTowardsPlayer());
    }

    private IEnumerator DashTowardsPlayer()
    {
        while (true)
        {
            yield return new WaitForSeconds(dashInterval);

            if (player != null)
            {
                Vector2 direction = (player.position - transform.position).normalized;
                rb.AddForce(direction * dashForce, ForceMode2D.Impulse);
            }
        }
    }
}
