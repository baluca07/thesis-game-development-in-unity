using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Player Stats UI")]
    [SerializeField] private TextMeshProUGUI healthText;
    //[SerializeField] private Text manaText;
    [SerializeField] private Image elementalIcon;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private Image timerFill;

    //[Header("Enemy Stats UI")]
    //[SerializeField] private Text enemyHealthText;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

    }

    public void UpdatePlayerHealth()
    {
        healthText.text = $"{PlayerStats.Instance.currentHealth}/{PlayerStats.Instance.maxHealth}";
    }

    public void UpdateElementalType()
    {
        UISpriteResolver resolver = elementalIcon.GetComponent<UISpriteResolver>();
        resolver.UpdateSprite(PlayerStats.Instance.currentElementalAttack.name);
    }

    public void StartCountRangedCooldown(float cooldown)
    {
        StartCoroutine(CountRangedCooldown(cooldown));
    }

    private IEnumerator CountRangedCooldown(float cooldown)
    {
        timerText.gameObject.SetActive(true);
        float timer = cooldown;

        while (timer > 0)
        {
            timer -= Time.deltaTime;
            timerText.text = "" + (int)timer;
            timerFill.fillAmount = timer / cooldown;
            yield return null; // Wait for the next frame
        }

        timerText.gameObject.SetActive(false);
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
