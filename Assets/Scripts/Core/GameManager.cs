using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ElementalAttack;

public class GameManager : MonoBehaviour
{
    public List<ElementalAttack> elementalAttacks = new List<ElementalAttack>();

    public ElementalAttack normalAttack = new ElementalAttack();

    public static GameManager Instance;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else
        {
            Destroy(gameObject);
        }
    }


    public void MovePlayerToRoom(Transform player, Transform spawnPoint)
    {
        if (spawnPoint == null)
        {
            Debug.LogError("Spawn point is not assigned!");
            return;
        }

        player.position = spawnPoint.position;
        Debug.Log($"Player moved to {spawnPoint.position} by GameManager!");
    }

    public void AddEnemyKill(ElementalDamageType type)
    {
        var attack = elementalAttacks.Find(a => a.type == type);
        if (attack != null)
        {
            attack.enemiesDefeated++;
            Debug.Log($"{PlayerStats.Instance.currentElementalAttack.type}");
            /*if (PlayerStats.Instance.currentElementalAttack.type == type)
            { 
                UIManager.Instance.UpdateLevelFill(); 
            }*/
            UpdateElementalAttackLevel(attack);
        }
    }
    private void UpdateElementalAttackLevel(ElementalAttack attack)
    {
        if (attack.currentLevel < attack.levels.Count - 1)
        {
            if (attack.enemiesDefeated >= attack.levels[attack.currentLevel + 1].requiredKills)
            {
                attack.currentLevel++;
                Debug.Log($"{attack.name} attack leveled up to level {attack.currentLevel}!");
                UIManager.Instance.SetLevelBar(attack.levels[attack.currentLevel].requiredKills, attack.levels[attack.currentLevel + 1].requiredKills);
                UIManager.Instance.UpdateElementalLevelText();
            }
        }
    }
    public void InitializeElementalAttacks(int normalLevel, int fireLevel, int waterLevel, int airLevel, int earthLevel)
    {
        normalAttack = new ElementalAttack
        {
            name = "Normal",
            type = ElementalDamageType.Normal,
            baseDamage = 5f,
            currentLevel = normalLevel,
            levels = new List<ElementalLevel>
        {
            new ElementalLevel { requiredKills = 10, damageBonus = 10 },
            new ElementalLevel { requiredKills = 20, damageBonus = 10 },
            new ElementalLevel { requiredKills = 50, damageBonus = 10 },
            new ElementalLevel { requiredKills = 100, damageBonus = 10 }
        }
        };
        Debug.Log($"{normalAttack.type}: {normalAttack.currentLevel} lvl");
        var fireAttack = new ElementalAttack
        {
            name = "Fire",
            type = ElementalDamageType.Fire,
            baseDamage = 0f,
            currentLevel = fireLevel,
            levels = new List<ElementalLevel>
        {
            new ElementalLevel { requiredKills = 0, damageBonus = 0 },
            new ElementalLevel { requiredKills = 5, damageBonus = 10 },
            new ElementalLevel { requiredKills = 10, damageBonus = 10 },
            new ElementalLevel { requiredKills = 20, damageBonus = 10 },
            new ElementalLevel { requiredKills = 50, damageBonus = 10 },
            new ElementalLevel { requiredKills = 100, damageBonus = 10 }
        }
        };

        var waterAttack = new ElementalAttack
        {
            name = "Water",
            type = ElementalDamageType.Water,
            baseDamage = 0f,
            currentLevel = waterLevel,
            levels = new List<ElementalLevel>
        {
            new ElementalLevel { requiredKills = 0, damageBonus = 0 },
            new ElementalLevel { requiredKills = 5, damageBonus = 10 },
            new ElementalLevel { requiredKills = 10, damageBonus = 10 },
            new ElementalLevel { requiredKills = 20, damageBonus = 10 },
            new ElementalLevel { requiredKills = 50, damageBonus = 10 },
            new ElementalLevel { requiredKills = 100, damageBonus = 10 }
        }
        };

        var airAtttack = new ElementalAttack
        {
            name = "Air",
            type = ElementalDamageType.Air,
            baseDamage = 0f,
            currentLevel = airLevel,
            levels = new List<ElementalLevel>
        {
            new ElementalLevel { requiredKills = 0, damageBonus = 0 },
            new ElementalLevel { requiredKills = 5, damageBonus = 10 },
            new ElementalLevel { requiredKills = 10, damageBonus = 10 },
            new ElementalLevel { requiredKills = 20, damageBonus = 10 },
            new ElementalLevel { requiredKills = 50, damageBonus = 10 },
            new ElementalLevel { requiredKills = 100, damageBonus = 10 }
        }
        };

        var earthAtttack = new ElementalAttack
        {
            name = "Earth",
            type = ElementalDamageType.Earth,
            baseDamage = 0f,
            currentLevel = earthLevel,
            levels = new List<ElementalLevel>
        {
            new ElementalLevel { requiredKills = 0, damageBonus = 0 },
            new ElementalLevel { requiredKills = 1, damageBonus = 10 },
            new ElementalLevel { requiredKills = 10, damageBonus = 10 },
            new ElementalLevel { requiredKills = 20, damageBonus = 10 },
            new ElementalLevel { requiredKills = 50, damageBonus = 10 },
            new ElementalLevel { requiredKills = 100, damageBonus = 10 }
        }
        };
        elementalAttacks.Add(fireAttack);
        elementalAttacks.Add(waterAttack);
        elementalAttacks.Add(airAtttack);
        elementalAttacks.Add(earthAtttack);

        PlayerPrefs.SetInt("NormalAttack",0);
        PlayerPrefs.SetInt("FireAttack",0);
        PlayerPrefs.SetInt("WaterAttack",0);
        PlayerPrefs.SetInt("AirAttack",0);
        PlayerPrefs.SetInt("EarthAttack",0);
        
        Debug.Log($"Elemental Attacks are initialized.");
        foreach (ElementalAttack attack in elementalAttacks)
        {
            Debug.Log($"{attack.type}: {attack.currentLevel} lvl");
        }
    }

    public void GameOver()
    {
        PlayerController.Instance.OnDisable();
        UIManager.Instance.ActivateGameOverScreen();
        Time.timeScale = 0;
        Debug.Log("Game Over!");
    }

    public void Win()
    {
        PlayerController.Instance.OnDisable();
        UIManager.Instance.ActivateWinScreen();
        Time.timeScale = 0;
        Debug.Log("Player Win!");
    }

    public void Pause()
    {
        PlayerController.Instance.OnDisable();
        UIManager.Instance.ActivatePauseScreen();
        Time.timeScale = 0;
        Debug.Log("Game Paused");
    }

    public void ContinueGame()
    {
        UIManager.Instance.DeactivatePauseScreen();
        Time.timeScale = 1;
        Debug.Log("Game Continued");
        PlayerController.Instance.OnEnable();
    }
    public void ResetLevel()
    {
        /* TODO - Fix this
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name
        );
        Time.timeScale = 1;*/
    }

    public void CompleteLevel(int dungeonIndex, int levelIndex)
    {
        PlayerPrefs.SetInt("Dungeon" + dungeonIndex + "Level" + levelIndex + "Completed", 1);
        SaveGame();
    }

    public void CompleteDungeon(int dungeonIndex)
    {
        PlayerPrefs.SetInt("Dungeon" + dungeonIndex, 1);
        SaveGame();
    }

    public void SaveGame()
    {
        Debug.Log("Saving game...");
        PlayerPrefs.SetString("SavedGame", System.DateTime.Now.ToString("yyyy/MM/dd HH-mm-ss"));
        PlayerPrefs.Save();
        Debug.Log("Saved!");
    }
}

