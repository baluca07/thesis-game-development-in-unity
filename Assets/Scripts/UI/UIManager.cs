using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;


    [Header("Header UI")]
    [SerializeField] private UIHealthBarManager healthBarManager;
    [SerializeField] private UIAttackStatManager[] attackStatManagers;
    [SerializeField] private List<Image> timerFills = new List<Image>();

    [Header("Header UI - PC")]
    [SerializeField] private Slider levelFill;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI damageText;
    [SerializeField] private Image elementalIcon;

    [Header("Quest UI")]
    [SerializeField] private TextMeshProUGUI enemies;

    [Header("Quest UI - PC Objects")]    
    [SerializeField] private TextMeshProUGUI rooms;

    [Header("WinSceen")]
    [SerializeField] private TextMeshProUGUI winScore;
    [SerializeField] private TextMeshProUGUI winTime;
    [SerializeField] private TextMeshProUGUI winDamageTaken;
    [SerializeField] private TextMeshProUGUI winDamageDealt;
    [SerializeField] private TextMeshProUGUI winKilledEnemies;

    [Header("GameOverSceen")]
    [SerializeField] private TextMeshProUGUI loseScore;
    [SerializeField] private TextMeshProUGUI loseTime;
    [SerializeField] private TextMeshProUGUI loseDamageTaken;
    [SerializeField] private TextMeshProUGUI loseDamageDealt;
    [SerializeField] private TextMeshProUGUI loseKilledEnemies;
    [SerializeField] private TextMeshProUGUI loseClearedRoom;

    [Header("Screens")]
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject pauseScreen;

    [Header("InputSystem")]
    [SerializeField] private InputAction pauseAction;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        attackStatManagers = FindObjectsOfType<UIAttackStatManager>();
    }

    private void Start()
    {
        DeactivateGameOverScreen();
        winScreen.SetActive(false);
        pauseAction = InputSystem.actions.FindAction("Pause");

#if UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX || UNITY_STANDALONE_LINUX
        elementalIcon = GameObject.Find("ElementalType").GetComponent<Image>();
        rooms = GameObject.Find("Rooms").GetComponent<TextMeshProUGUI>();
#endif
        Image[] allImage = FindObjectsOfType<Image>();
        foreach (Image image in allImage)
        {
            if(image.name == "TimerFill")
            {
                timerFills.Add(image);
            }
        }
       
    }

    private void Update()
    {
        if (pauseAction.WasPerformedThisFrame())
        {
            GameManager.Instance.Pause(); ;
        }
    }
    public void UpdatePlayerHealth()
    {
        if(healthBarManager != null)
        {
            healthBarManager.UpdatePlayerHealthFill();
        }
        else
        {
            healthBarManager = GetComponentInChildren<UIHealthBarManager>();
        }
    }
    public void UpdateAttackStats()
    {
#if UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX || UNITY_STANDALONE_LINUX
        if (PlayerStats.Instance.currentElementalAttack.currentLevel < PlayerStats.Instance.currentElementalAttack.levels.Count - 1)
           {
               levelFill.value = PlayerStats.Instance.currentElementalAttack.enemiesDefeated;
           }
           else
           {
               levelFill.value = levelFill.maxValue;
           }
#endif
        foreach(UIAttackStatManager script in attackStatManagers)
        {
            script.UpdateAll();
        }
    }

#if UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX || UNITY_STANDALONE_LINUX

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
#endif

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
            foreach (var timerFill in timerFills)
            {
                if (timerFill != null)
                {
                    timerFill.fillAmount = timer / cooldown;
                }
            }
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

    public void UpdateWinScreenData(int killedEnemies, int damageDealt, int damageTaken, float time, int score)
    {
        winKilledEnemies.text = killedEnemies.ToString();
        winDamageDealt.text = damageDealt.ToString();
        winDamageTaken.text = damageTaken.ToString();
        TimeSpan timeSpan = TimeSpan.FromSeconds(time);
        winTime.text = string.Format("{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds);
        winScore.text = score.ToString();
    }

    public void UpdateGameOverScreenData(int killedEnemies, int damageDealt, int damageTaken, float time, int score)
    {
        loseKilledEnemies.text = killedEnemies.ToString();
        loseDamageDealt.text = damageDealt.ToString();
        loseDamageTaken.text = damageTaken.ToString();
        TimeSpan timeSpan = TimeSpan.FromSeconds(time);
        loseTime.text = string.Format("{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds);
#if UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX || UNITY_STANDALONE_LINUX
        loseClearedRoom.text = DungeonController.Instance.clearedRooms.ToString();
#endif
        loseScore.text = score.ToString();
    }
}
