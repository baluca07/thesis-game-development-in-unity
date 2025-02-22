using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Header UI")]
    [SerializeField] private Image healthFill;
    [SerializeField] private Image levelFill;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI damageText;
    //[SerializeField] private Text manaText;
    [SerializeField] private Image elementalIcon;
    [SerializeField] private Image timerFill;

    //[Header("Quest UI")]
    //[Header("Enemy Stats UI")]
    //[SerializeField] private Text enemyHealthText;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void UpdatePlayerHealthFill()
    {
        healthFill.fillAmount = PlayerStats.Instance.currentHealth/ PlayerStats.Instance.maxHealth;
    }
    public void UpdateLevelFill()
    {
        if (PlayerStats.Instance.currentElementalAttack.currentLevel < PlayerStats.Instance.currentElementalAttack.levels.Count - 1)
        {
            levelFill.fillAmount = (float)PlayerStats.Instance.currentElementalAttack.enemiesDefeated / PlayerStats.Instance.currentElementalAttack.levels[PlayerStats.Instance.currentElementalAttack.currentLevel + 1].requiredKills;
        }
        else
        {
            levelFill.fillAmount = 1f;
        }
    }

    public void UpdateElementalTypeIcon()
    {
        UISpriteResolver resolver = elementalIcon.GetComponent<UISpriteResolver>();
        resolver.UpdateSprite(PlayerStats.Instance.currentElementalAttack.name);
        if(PlayerStats.Instance.currentElementalAttack.currentLevel == 0)
        {
            resolver.UpdateSprite(PlayerStats.Instance.currentElementalAttack.name + "Disabled");
        }
        else
        {
            resolver.UpdateSprite(PlayerStats.Instance.currentElementalAttack.name);
        }
        UpdateElementalLevelText();
    }

    public void UpdateElementalLevelText()
    {
        if (PlayerStats.Instance.currentElementalAttack.currentLevel < PlayerStats.Instance.currentElementalAttack.levels.Count - 1)
        {
            levelText.text = $"LVL {PlayerStats.Instance.currentElementalAttack.currentLevel}";
        }
        else
        {
            levelText.text = "MAX";
        }
        UpdateLevelFill();
        UpdateDamageText();
    }

    public void UpdateDamageText()
    {
        damageText.text = $"DMG: {PlayerStats.Instance.currentElementalAttack.GetDamage().amount}";
    }

    public void StartCountRangedCooldown(float cooldown)
    {
        StartCoroutine(CountRangedCooldown(cooldown));
    }

    private IEnumerator CountRangedCooldown(float cooldown)
    {
        float timer = cooldown;

        while (timer > 0)
        {
            timer -= Time.deltaTime;
            timerFill.fillAmount = timer / cooldown;
            yield return null; // Wait for the next frame
        }
    }


    /*public void UpdatePlayerMana(int currentMana, int maxMana)
    {
        manaText.text = $"Mana: {currentMana}/{maxMana}";
    }*/

    /*public void UpdateEnemyHealth(int currentHealth, int maxHealth)
    {
        enemyHealthText.text = $"Enemy Health: {currentHealth}/{maxHealth}";
    }*/
}
