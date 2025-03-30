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
        Debug.Log($"{type} Current level: {currentLevel} {baseDamage}");
        int bonusDamage = levels[currentLevel].damageBonus;
        return new Damage(type, baseDamage + bonusDamage);
    }

    public void LevelUp()
    {
        if (currentLevel < levels.Count - 1)
        {
            if (enemiesDefeated >= levels[currentLevel + 1].requiredKills)
            {
                currentLevel++;
                Debug.Log($"{name} attack leveled up to level {currentLevel}!");
#if UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX || UNITY_STANDALONE_LINUX
                UIManager.Instance.SetLevelBar(levels[currentLevel].requiredKills, levels[currentLevel + 1].requiredKills);
                UIManager.Instance.UpdateElementalLevelText();
#endif
                UIManager.Instance.UpdateAttackStats();
            }
        }
    }
}

