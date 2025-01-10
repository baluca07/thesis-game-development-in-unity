
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyData enemyData;
    public int currentHealth;

    void Start()
    {
        currentHealth = enemyData.health;
        //Debug.Log($"Enemy Name: {enemyData.enemyName}");
        //Debug.Log($"Max Health: {enemyData.health}");s
        //Debug.Log($"Current Health: {currentHealht}");
        //Debug.Log($"Base Damage: {enemyData.baseDamage}");
        //Debug.Log($"Damage Category: {enemyData.damageCategory}");
        //Debug.Log($"Damage Elemental Type:  {enemyData.elementalDamageType}");
        //Debug.Log($"Physical Shield: {enemyData.physicalShield}");
        //Debug.Log($"Magical Shield: {enemyData.magicShield}");
    }
    public virtual void Attack(PlayerStats player)
    {
        Debug.Log($"{enemyData.enemyName} attacks Player.");
    }



    public void TakeDamage(Damage damage)
    {
        int damageAmoun = damage.CalculateDamageOnEnemy(enemyData);

        currentHealth -= damageAmoun;

        Debug.Log($"{enemyData.enemyName} took {damageAmoun} damage! Current Health: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }


    private void Die()
    {
        Debug.Log($"{enemyData.enemyName} has been defeated!");
        Destroy(gameObject);
    }
}
