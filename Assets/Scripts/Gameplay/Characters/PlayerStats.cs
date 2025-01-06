using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStats
{
    public int MaxHealth = 100;
    public int CurrentHealth;
    public int Damage = 10;
    public DamageCategory CurrentDamageCategory = DamageCategory.Physical;
    public ElementalDamageType currentElementalDamageType = ElementalDamageType.Normal;

    public void TakeDamage(int amount)
    {
        CurrentHealth -= amount;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);
    }

    public void Heal(int amount)
    {
        CurrentHealth += amount;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);
    }
}

