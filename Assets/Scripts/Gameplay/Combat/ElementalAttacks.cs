using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ElementalAttack
{
    public string name;
    public ElementalDamageType type;
    public float baseDamage;
    public int enemiesDefeated=0;
    public int currentLevel = 0;
    public class ElementalLevel
    {
        public int requiredKills;
        public int damageBonus;
    }

    public List<ElementalLevel> levels;

    public Damage GetDamage()
    {
        int bonusDamage = 0;
        foreach (var level in levels)
        {
            if (enemiesDefeated >= level.requiredKills)
            {
                bonusDamage = level.damageBonus;
            }
            else
            {
                break; // Ha a következõ szinthez nincs elég kill, kilépünk
            }
        }
        return new Damage(type, baseDamage + bonusDamage);
    }
}

