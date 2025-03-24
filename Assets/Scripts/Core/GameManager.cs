using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using static ElementalAttack;

public class GameManager : MonoBehaviour
{
    public List<ElementalAttack> elementalAttacks = new List<ElementalAttack>();

    public ElementalAttack normalAttack = new ElementalAttack();


    public RoomController currentRoom;

    private DynamicIsometricCameraFollow cameraController;
    public Vector2 playerSpawnpoint = new Vector2(0,0);

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
#if UNITY_ANDROID || UNITY_IOS
        InitializeMobileLevelSpawnpoints();
#endif
        //Just for testing
        InitializeElementalAttacks(0,1,0,0,0);
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
#if UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX || UNITY_STANDALONE_LINUX
            Debug.Log($"{PlayerStats.Instance.currentElementalAttack.type}");
            if (PlayerStats.Instance.currentElementalAttack.type == type)
            { 
                UIManager.Instance.UpdateLevelFill(); 
            }
            attack.LevelUp();
#elif UNITY_ANDROID || UNITY_IOS
            attack.LevelUp();
#endif
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
            new ElementalLevel { requiredKills = 5, damageBonus = 10 },
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
            new ElementalLevel { requiredKills = 1, damageBonus = 10 },
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
            new ElementalLevel { requiredKills = 1, damageBonus = 10 },
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
            new ElementalLevel { requiredKills = 1, damageBonus = 10 },
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
#if UNITY_ANDROID || UNITY_IOS

    public Dictionary<int, Dictionary<int, Vector2>> mobileSpawnpoints = new Dictionary<int, Dictionary<int, Vector2>>();
    public void InitializeMobileLevelSpawnpoints()
    {
        Debug.Log("Initialite mobile spawnpoints...");
        mobileSpawnpoints.Clear();
        Dictionary<int, Vector2> dungeon1Levels = new Dictionary<int, Vector2>();
        dungeon1Levels.Add(1, new Vector2(-4f, -2f));
        dungeon1Levels.Add(2, new Vector2(10.5f, 7.5f));
        dungeon1Levels.Add(3, new Vector2(-14.5f, 5.5f));
        Debug.Log($"Initialized {dungeon1Levels.Count} level to dungeon1");
        mobileSpawnpoints.Add(1, dungeon1Levels);
        Debug.Log($"Initilaized {mobileSpawnpoints.Count} mobile dungeon");
    }

    public void SetSpawnpoint(int dungeonID, int levelID)
    {
        playerSpawnpoint =  mobileSpawnpoints[dungeonID][levelID];
        Debug.LogWarning($"PlayerSpawnpoint set to: {playerSpawnpoint}");
    }
#endif
    public void GameOver()
    {
        PlayerController.Instance.OnDisable();
        int score = SessionController.Instance.CalculateScore();
        UIManager.Instance.UpdateGameOverScreenData(SessionController.Instance.killedEnemiesCount,
                                                SessionController.Instance.dealtDamage,
                                                SessionController.Instance.takenDamage,
                                                SessionController.Instance.sessionTime,
                                                score);
        UIManager.Instance.ActivateGameOverScreen();
        Time.timeScale = 0;
        Debug.Log("Game Over!");
        SessionController.Instance.EndSession();
    }

    public void Win()
    {
        PlayerController.Instance.OnDisable();
        UIManager.Instance.ActivateWinScreen();
        Time.timeScale = 0;
        Debug.Log("Player Win!");
        SessionController.Instance.EndSession();
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

#if UNITY_ANDROID || UNITY_IOS
    public void CompleteLevel(int dungeonIndex, int levelIndex)
    {
        PlayerPrefs.SetInt("Dungeon" + dungeonIndex + "Level" + levelIndex + "Completed", 1);
        int score = SessionController.Instance.CalculateScore();
        PlayerPrefs.SetInt("Dungeon" + dungeonIndex + "Level" + levelIndex + "Score", score);
        UIManager.Instance.UpdateWinScreenData(SessionController.Instance.killedEnemiesCount, 
                                                SessionController.Instance.dealtDamage, 
                                                SessionController.Instance.takenDamage, 
                                                SessionController.Instance.sessionTime, 
                                                score);
        int stars = StarDisplay.Instance.CalculateStars(score);
        PlayerPrefs.SetInt("Dungeon" + dungeonIndex + "Level" + levelIndex + "Stars", stars);
        StarDisplay.Instance.DisplayStars(stars);
        SaveGame();
        Win();
    }
#endif

    public void CompleteDungeon(int dungeonIndex)
    {
        PlayerPrefs.SetInt("Dungeon" + dungeonIndex + "Completed", 1);
        int score = SessionController.Instance.CalculateScore();
        PlayerPrefs.SetInt("Dungeon" + dungeonIndex + "Score", score);
        UIManager.Instance.UpdateWinScreenData(SessionController.Instance.killedEnemiesCount,
                                                SessionController.Instance.dealtDamage,
                                                SessionController.Instance.takenDamage,
                                                SessionController.Instance.sessionTime, 
                                                score);
        SaveGame();
        Win();
    }

    public void SaveGame()
    {
        Debug.Log("Saving game...");
        PlayerPrefs.SetString("SavedGame", System.DateTime.Now.ToString("yyyy/MM/dd HH-mm-ss"));
        PlayerPrefs.Save();
        Debug.Log("Saved!");
    }

    public void SpawnPlayer()
    {
        PlayerController.Instance.transform.position = playerSpawnpoint;
        Debug.Log($"Player spawned to {PlayerController.Instance.transform}");
    }

    public void EnterRoom()
    {
        cameraController = Camera.main.GetComponent<DynamicIsometricCameraFollow>();
        if (cameraController == null)
        {
            Debug.LogError("DynamicIsometricCameraFollow script not found on the main camera.");
        }
        if (currentRoom == null)
        {
            Debug.LogError("Current room is missing!");
        }
        else
        {
            cameraController.UpdateBoundaries();
        }
    }


}

