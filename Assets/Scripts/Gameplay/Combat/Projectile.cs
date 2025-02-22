using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] GameObject PartilcePrefab;
    public enum ProjectileOwner
    {
        Enemy,
        Player
    }

    public ProjectileOwner owner;

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
            Instantiate(PartilcePrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
            Debug.Log("Projectile destroyed: exceeded attack range");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log($"Object triggered: {collision.name}");

        if (owner == ProjectileOwner.Enemy && collision.CompareTag("Enemy"))
        {
            return;
        }

        if (owner == ProjectileOwner.Player && collision.CompareTag("Player"))
        {
            return;
        }

        if (collision.CompareTag("Enemy"))
        {
            Instantiate(PartilcePrefab, transform.position, Quaternion.identity);
            EnemyStats enemy = collision.GetComponent<EnemyStats>();
            Debug.Log($"Enemy collided: {enemy.enemyName}");
           
            enemy.TakeDamage(damage);
            Destroy(gameObject);
            Debug.Log($"Enemy damaged: {enemy.enemyName}");
        }

        if (collision.CompareTag("Player"))
        {
            PlayerStats player = collision.GetComponent<PlayerStats>();
            Debug.Log($"Player collided.");

            player.TakeDamage(damage);
            Destroy(gameObject);
            Debug.Log($"Player Damaged");
        }
    }
}
