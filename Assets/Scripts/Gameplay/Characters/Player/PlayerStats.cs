using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance;

    public int maxHealth = 100;
    public int currentHealth;
    public ElementalAttack currentElementalAttack;
    public int currentElementalAttackIndex;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

    }

    private void Start()
    {
        currentHealth = maxHealth;
        UIManager.Instance.UpdatePlayerHealthFill();
        SetCurrentElemental(0);
    }

    public void TakeDamage(Damage damage)
    {
        int damageAmoun = damage.CalculateDamageOnPlayer(this);

        currentHealth -= damageAmoun;

        Debug.Log($"Player took {damageAmoun} damage! Current Health: {currentHealth}");

        UIManager.Instance.UpdatePlayerHealthFill();

        if (currentHealth <= 0)
        {
            GameManager.Instance.GameOver();
        }
    }

    /*public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
    }*/

    public void SetCurrentElemental(int index)
    {
        currentElementalAttackIndex = index;
        Debug.Log($"Set elemental to index:{ index}");
        currentElementalAttack = (GameManager.Instance.elementalAttacks[index]);
        UIManager.Instance.UpdateElementalTypeIcon();
        UIManager.Instance.UpdateElementalLevelText();
    }
    
}

