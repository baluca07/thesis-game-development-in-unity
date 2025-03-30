using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIAttackStatManager : MonoBehaviour
{
    public ElementalDamageType damageType;

    [SerializeField] Slider levelFill;
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI damageText;

    private void Start()
    {
        levelFill = GetComponentInChildren<Slider>();
    }
    private ElementalAttack FindElementalAttack()
    {
        foreach (ElementalAttack attack in GameManager.Instance.elementalAttacks)
        {
            if (attack.type == damageType)
            {
                return attack;
            }
        }
        return null;
    }

    public void UpdateLevelText()
    {
        int level;
        if(damageType == ElementalDamageType.Normal)
        {
            level = GameManager.Instance.normalAttack.currentLevel;
            levelText.text = $"LVL {level}";
        }
        else
        {
            ElementalAttack attack = FindElementalAttack();
            if (attack.currentLevel == attack.levels.Count - 1)
            {
                levelText.text = $"LVL MAX";
            }
            else
            { 
                level = attack.currentLevel;
                levelText.text = $"LVL {level}";
            }
        }
    }

    public void UpdateDamageText()
    {
        float damage;
        if (damageType == ElementalDamageType.Normal)
        {
            damage = GameManager.Instance.normalAttack.GetDamage().amount;
        }
        else
        {
            ElementalAttack attack = FindElementalAttack();
            damage = attack.GetDamage().amount;
        }

        damageText.text = $"DMG: {damage}";
    }

    public void UpdateLevelFill()
    {
        ElementalAttack attack;
        if (damageType == ElementalDamageType.Normal)
        {
            attack = GameManager.Instance.normalAttack;
            if (attack.currentLevel == attack.levels.Count - 1)
            {
                levelFill.value = levelFill.maxValue;
            }
            else
            {
                levelFill.value = attack.enemiesDefeated;
            }
        }
        else
        {
            attack = FindElementalAttack();
            if (attack.currentLevel == attack.levels.Count - 1)
            {
                levelFill.value = levelFill.maxValue;
            }
            else
            {
                levelFill.value = attack.enemiesDefeated;
            }
        }
       
    }

    public void SetLevelFillBoundaries()
    {
        ElementalAttack attack;
        if (damageType == ElementalDamageType.Normal)
        {
            attack = GameManager.Instance.normalAttack;
            if (attack.currentLevel == 0)
            {
                levelFill.minValue = 0;
                levelFill.maxValue = attack.levels[attack.currentLevel + 1].requiredKills;
            }
            else if (attack.currentLevel == attack.levels.Count - 1)
            {
                levelFill.maxValue = attack.levels[attack.currentLevel].requiredKills;
            }
            else
            {
                levelFill.minValue = attack.levels[attack.currentLevel].requiredKills;
                levelFill.maxValue = attack.levels[attack.currentLevel + 1].requiredKills;
            }
        }
        else
        {
            attack = FindElementalAttack();
            if (attack.currentLevel == 0)
            {
                levelFill.minValue = 0;
                levelFill.maxValue = attack.levels[attack.currentLevel + 1].requiredKills;
            }
            else if (attack.currentLevel == attack.levels.Count - 1)
            {
                levelFill.maxValue = attack.levels[attack.currentLevel].requiredKills;
            }
            else
            {
                levelFill.minValue = attack.levels[attack.currentLevel].requiredKills;
                levelFill.maxValue = attack.levels[attack.currentLevel + 1].requiredKills;
            }
        }
    }

    public void UpdateAll()
    {
        SetLevelFillBoundaries();
        UpdateDamageText();
        UpdateLevelFill();
        UpdateLevelText();
    }
}
