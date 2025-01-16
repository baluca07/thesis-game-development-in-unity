using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    public int maxHealth = 100;
    public int currentHealth;

    public UIManager uimanager;

    private void Start()
    {
        currentHealth = maxHealth;
        uimanager = GameObject.FindAnyObjectByType<UIManager>();
    }

    public void TakeDamage(Damage damage)
    {
        int damageAmoun = damage.CalculateDamageOnPlayer(this);

        currentHealth -= damageAmoun;

        Debug.Log($"Player took {damageAmoun} damage! Current Health: {currentHealth}");

        uimanager.UpdatePlayerHealth(currentHealth,maxHealth);

        if (currentHealth <= 0)
        {
            Debug.Log("Game Over!");
        }
    }

    /*public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
    }*/
}

