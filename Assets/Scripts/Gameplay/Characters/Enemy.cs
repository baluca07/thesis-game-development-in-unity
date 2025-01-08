
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyData enemyData;
    public int currentHealht;

    void Start()
    {
        currentHealht = enemyData.health;
        //Debug.Log($"Enemy Name: {enemyData.enemyName}");
        //Debug.Log($"Max Health: {enemyData.health}");
        //Debug.Log($"Current Health: {currentHealht}");
        //Debug.Log($"Base Damage: {enemyData.baseDamage}");
        //Debug.Log($"Damage Category: {enemyData.damageCategory}");
        //Debug.Log($"Damage Elemental Type:  {enemyData.elementalDamageType}");
        //Debug.Log($"Physical Shield: {enemyData.physicalShield}");
        //Debug.Log($"Magical Shield: {enemyData.magicShield}");
    }


    public void TakeDamage(Damage damage)
    {
        int damageAmoun = damage.CalculateDamageOnEnemy(enemyData);

        currentHealht -= damageAmoun;

        //Debug.Log($"{enemyData.enemyName} took {damageAmoun} damage! Current Health: {currentHealht}");

        if (currentHealht <= 0)
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
