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
        Debug.Log($"Current level: {currentLevel} {baseDamage} {type}");
        //int bonusDamage = levels[currentLevel].damageBonus;
        int bonusDamage = 0;
        return new Damage(type, baseDamage + bonusDamage);
    }
}

