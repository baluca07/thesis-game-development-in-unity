using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Header UI")]
    [SerializeField] private Slider healthFill;
    [SerializeField] private Slider levelFill;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI damageText;

    [SerializeField] private Image elementalIcon;
    [SerializeField] private Image timerFill;

    [Header("Quest UI")]
    [SerializeField] private TextMeshProUGUI enemies;
#if UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX || UNITY_STANDALONE_LINUX
    [SerializeField] private TextMeshProUGUI rooms;
#endif

    //[Header("Enemy Stats UI")]
    [Header("Screens")]
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject pauseScreen;
    //[SerializeField] private Text enemyHealthText;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        healthFill.minValue = 0;
        healthFill.maxValue = PlayerStats.Instance.maxHealth;
        healthFill.value = PlayerStats.Instance.currentHealth;
        DeactivateGameOverScreen();
        winScreen.SetActive(false);
    }

    public void UpdatePlayerHealthFill()
    {
        healthFill.value = PlayerStats.Instance.currentHealth;
    }
    public void UpdateLevelFill()
    {
        if (PlayerStats.Instance.currentElementalAttack.currentLevel < PlayerStats.Instance.currentElementalAttack.levels.Count - 1)
        {
            levelFill.value = PlayerStats.Instance.currentElementalAttack.enemiesDefeated;
        }
        else
        {
            levelFill.value = levelFill.maxValue;
        }
    }

    public void SetLevelBar(int min, int max)
    {
        levelFill.minValue = min;
        levelFill.maxValue = max;
    }

    public void UpdateElementalTypeIcon()
    {
        UISpriteResolver resolver = elementalIcon.GetComponent<UISpriteResolver>();
        resolver.UpdateSprite(PlayerStats.Instance.currentElementalAttack.name);
        if(PlayerStats.Instance.currentElementalAttack.currentLevel == 0)
        {
            resolver.UpdateSprite(PlayerStats.Instance.currentElementalAttack.name + "Disabled");
            SetLevelBar(0,5);
            UpdateLevelFill();
        }
        else if(PlayerStats.Instance.currentElementalAttack.currentLevel < PlayerStats.Instance.currentElementalAttack.levels.Count - 1)
        {
            resolver.UpdateSprite(PlayerStats.Instance.currentElementalAttack.name);
            SetLevelBar(PlayerStats.Instance.currentElementalAttack.levels[PlayerStats.Instance.currentElementalAttack.currentLevel].requiredKills,
                PlayerStats.Instance.currentElementalAttack.levels[PlayerStats.Instance.currentElementalAttack.currentLevel + 1].requiredKills);
            UpdateLevelFill();
        }
        else
        {
            resolver.UpdateSprite(PlayerStats.Instance.currentElementalAttack.name);
            levelFill.value = levelFill.maxValue;
            UpdateLevelFill();
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
            yield return null;
        }
    }
    public void UpdateQuestEnemies(int maxEnemyCount, int currentEnemyCount)
    {
        enemies.text = $"Remained Enemies: {currentEnemyCount}/{maxEnemyCount}";
        Debug.Log($"Updated Quest: remained enemies {maxEnemyCount}");
    }
#if UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX || UNITY_STANDALONE_LINUX
    public void UpdateQuestRooms()
    {
        rooms.text = $"Remained Rooms: {DungeonController.Instance.clearedRooms}/{DungeonController.Instance.roomsCount}";
    }
#endif

    public void ActivateGameOverScreen()
    {
        gameOverScreen.SetActive(true);
    }

    public void DeactivateGameOverScreen()
    {
        gameOverScreen.SetActive(false);
    }

    public void ActivateWinScreen()
    {
        winScreen.SetActive(true);
    }

    public void ActivatePauseScreen()
    {
        pauseScreen.SetActive(true);
    }

    public void DeactivatePauseScreen()
    {
        pauseScreen.SetActive(false);
    }

    public void PauseGame(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
        {
            GameManager.Instance.Pause();
        }
    }
}
