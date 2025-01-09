using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Damage damage;
    public float attackRange;
    public float speed;

    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        // Move the projectile forward
        transform.Translate(Vector3.right * speed * Time.deltaTime);

        // Check if the projectile has exceeded the attack range
        if (Vector3.Distance(startPosition, transform.position) >= attackRange)
        {
            Destroy(gameObject);
            Debug.Log("Projectile destroyed: exceeded attack range");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"Object triggered: {collision.name}");

        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            Debug.Log($"Enemy collided: {enemy.enemyData.enemyName}");
           
            enemy.TakeDamage(damage);
            Destroy(gameObject);
            Debug.Log($"Enemy damaged: {enemy.enemyData.enemyName}");
        }
    }
}
