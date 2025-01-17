using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Player Stats UI")]
    [SerializeField] private TextMeshProUGUI healthText;
    //[SerializeField] private Text manaText;
    [SerializeField] private TextMeshProUGUI elementalTypeText;

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
        elementalTypeText.text = $"{PlayerStats.Instance.currentElementalAttack.name}";
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
