using System;
using System.Diagnostics;

public class Damage
{
    public ElementalDamageType elementalType;

    public float amount;

    public Damage(ElementalDamageType elementalType, float amount)
    {
        this.elementalType = elementalType;
        this.amount = amount;
    }

    public int CalculateDamageOnEnemy(EnemyStats enemy)
    {
        float finalDamage = amount;

        float elementalMultiplier = ElementalDamageSystem.GetElementalDamageMultiplier(elementalType, enemy.elementalDamageType);

        finalDamage *= elementalMultiplier;

        return (int)Math.Round(finalDamage);

    }

    public int CalculateDamageOnPlayer(PlayerStats player)
    {
        float finalDamage = amount;

        float elementalMultiplier = ElementalDamageSystem.GetElementalDamageMultiplier(elementalType,ElementalDamageType.Normal);

        finalDamage *= elementalMultiplier;

        return (int)Math.Round(finalDamage);

    }
}
