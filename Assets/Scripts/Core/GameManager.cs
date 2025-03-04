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

        InitializeElementalAttacks();
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
    private void InitializeElementalAttacks()
    {
        normalAttack = new ElementalAttack
        {
            name = "Normal",
            type = ElementalDamageType.Normal,
            baseDamage = 5f,
            currentLevel = 0,
            levels = new List<ElementalLevel>
        {
            new ElementalLevel { requiredKills = 10, damageBonus = 10 },
            new ElementalLevel { requiredKills = 20, damageBonus = 10 },
            new ElementalLevel { requiredKills = 50, damageBonus = 10 },
            new ElementalLevel { requiredKills = 100, damageBonus = 10 }
        }
        };
        Debug.Log($"Current level: {normalAttack.currentLevel} {normalAttack.baseDamage} {normalAttack.type}");
        var fireAttack = new ElementalAttack
        {
            name = "Fire",
            type = ElementalDamageType.Fire,
            baseDamage = 0f,
            currentLevel = 1,
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
            currentLevel = 0,
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
            currentLevel = 0,
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
            currentLevel = 0,
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
        Debug.Log($"Elemental Attacks are initialized. Count of elemental attacks: {elementalAttacks.Count}");
    }

    public void GameOver()
    {
        UIManager.Instance.ActivateGameOverScreen();
        Time.timeScale = 0;
        PlayerController.Instance.OnDisable();
        Debug.Log("Game Over!");
    }
    public void ResetGame()
    {
        // Reload the current scene
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name
        );
        Time.timeScale = 1;
    }
}

