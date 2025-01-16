
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [Header("Basic Stats")]
    public string enemyName;
    public int health;
    public float speed;
    public int baseDamage;
    //public float attackTime;
    public float attackCoolDown;
    public float attackRange;
    //public Range range;

    [Header("Damage Type")]
    public ElementalDamageType elementalDamageType;

    public int currentHealth;

    void Start()
    {
        currentHealth = health;
    }
    public void TakeDamage(Damage damage)
    {
        int damageAmoun = damage.CalculateDamageOnEnemy(this);

        currentHealth -= damageAmoun;

        Debug.Log($"{enemyName} took {damageAmoun} damage! Current Health: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }


    private void Die()
    {
        Debug.Log($"{enemyName} has been defeated!");
        GameManager.Instance.AddEnemyKill(elementalDamageType);
        Destroy(gameObject);
    }



}
