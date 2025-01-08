using System;
using System.Diagnostics;

public class Damage
{
    public DamageCategory damageCategory;
    public ElementalDamageType elementalType;

    public float amount;

    public Damage(DamageCategory damageCategory, ElementalDamageType elementalType, float amount)
    {
        this.damageCategory = damageCategory;
        this.elementalType = elementalType;
        this.amount = amount;
    }

    public int CalculateDamageOnEnemy(EnemyData enemy)
    {
        float finalDamage = amount;

        float elementalMultiplier = ElementalDamageSystem.GetElementalDamageMultiplier(elementalType, enemy.elementalDamageType);

        finalDamage *= elementalMultiplier;

        if (damageCategory == DamageCategory.Physical)
        {
            finalDamage -=(float)enemy.physicalShield;
        }
        else if (damageCategory == DamageCategory.Magical)
        {
            finalDamage -= (float)enemy.magicShield;
        }

        return (int)Math.Round(finalDamage);

    }
}
