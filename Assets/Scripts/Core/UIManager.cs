using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Player Stats UI")]
    [SerializeField] private TextMeshPro healthText;
    //[SerializeField] private Text manaText;
    [SerializeField] private TextMeshPro elementalTypeText;

    //[Header("Enemy Stats UI")]
    //[SerializeField] private Text enemyHealthText;

    public void UpdatePlayerHealth(int currentHealth, int maxHealth)
    {
        healthText.text = $"Health: {currentHealth}/{maxHealth}";
    }

    public void UpdateElementalType(string elementalType)
    {
        elementalTypeText.text = $"Element: {elementalType}";
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
