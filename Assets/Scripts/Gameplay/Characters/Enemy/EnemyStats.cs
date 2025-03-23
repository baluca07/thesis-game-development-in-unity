
using UnityEngine;
using static GameManager;

public class EnemyStats : MonoBehaviour
{
    [Header("Basic Stats")]
    public string enemyName;
    public int health;
    public int baseDamage;
    public float attackRange = 1.2f;

    [Header("Damage Type")]
    public ElementalDamageType elementalDamageType;

    public int currentHealth;

    private Animator anim;

    void Start()
    {
        currentHealth = health;
        anim = GetComponent<Animator>();
    }
    public void TakeDamage(Damage damage)
    {
        int damageAmount = damage.CalculateDamageOnEnemy(this);

        currentHealth -= damageAmount;

        Debug.Log($"{enemyName} took {damageAmount} damage! Current Health: {currentHealth}");
        SessionController.Instance.AddDealtDamage(damageAmount);
        anim.SetTrigger("Damage");

        if (currentHealth <= 0)
        {
            Die();
        }
    }


    private void Die()
    {
        Debug.Log($"{enemyName} has been defeated!");
        GameManager.Instance.AddEnemyKill(elementalDamageType);
        SessionController.Instance.IncrementKilledEnemies();


        GameManager.Instance.currentRoom.EnemyDefeated();
        Destroy(gameObject);
    }



}
