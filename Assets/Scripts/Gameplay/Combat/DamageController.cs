using System;
using System.Diagnostics;

public class DamageController
{
    public DamageCategory damageCategory;
    public ElementalDamageType elementalType;

    public float amount;
 
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
