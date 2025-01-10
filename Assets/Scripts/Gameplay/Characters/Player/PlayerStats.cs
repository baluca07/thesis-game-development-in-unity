using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public ElementalDamageType damageType;

    [Header("Damage Category")]
    public DamageCategory damageCategory;

    [Header("Damage Type")]
    public ElementalDamageType elementalDamageType;

    [Header("Defense Stats")]
    public int physicalShield;
    public int magicShield;

    //TODO - Set Player stats

    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(Damage damage)
    {
        int damageAmoun = damage.CalculateDamageOnPlayer(this);

        currentHealth -= damageAmoun;

        Debug.Log($"Player took {damageAmoun} damage! Current Health: {currentHealth}");

        if (currentHealth <= 0)
        {
            Debug.Log("Game Over!");
        }
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
    }
}

