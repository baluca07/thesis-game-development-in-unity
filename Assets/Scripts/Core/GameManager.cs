using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ElementalAttack;

public class GameManager : MonoBehaviour
{
    public List<ElementalAttack> elementalAttacks = new List<ElementalAttack>();

    public static GameManager Instance;

    [SerializeField] string elementalAttackName;
    [SerializeField] int defeteatedEnemies;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
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
        }
    }
    void InitializeElementalAttacks()
    {
        var normalAttack = new ElementalAttack
        {
            name = "Normal",
            type = ElementalDamageType.Normal,
            baseDamage = 5,
            levels = new List<ElementalLevel>
        {
            new ElementalLevel { requiredKills = 0, damageBonus = 0 },
            new ElementalLevel { requiredKills = 20, damageBonus = 10 },
            new ElementalLevel { requiredKills = 50, damageBonus = 10 },
            new ElementalLevel { requiredKills = 100, damageBonus = 10 }
        }
        };
        var fireAttack = new ElementalAttack
        {
            name = "Fire",
            type = ElementalDamageType.Fire,
            baseDamage = 10,
            levels = new List<ElementalLevel>
        {
            new ElementalLevel { requiredKills = 0, damageBonus = 0 }, //for test
            new ElementalLevel { requiredKills = 10, damageBonus = 0 },
            new ElementalLevel { requiredKills = 20, damageBonus = 10 },
            new ElementalLevel { requiredKills = 50, damageBonus = 10 },
            new ElementalLevel { requiredKills = 100, damageBonus = 10 }
        }
        };

        var waterAttack = new ElementalAttack
        {
            name = "Water",
            type = ElementalDamageType.Water,
            baseDamage = 8,
            levels = new List<ElementalLevel>
        {
            new ElementalLevel { requiredKills = 0, damageBonus = 0 }, //for test
            new ElementalLevel { requiredKills = 10, damageBonus = 0 },
            new ElementalLevel { requiredKills = 20, damageBonus = 10 },
            new ElementalLevel { requiredKills = 50, damageBonus = 10 },
            new ElementalLevel { requiredKills = 100, damageBonus = 10 }
        }
        };

        var airAtttack = new ElementalAttack
        {
            name = "Air",
            type = ElementalDamageType.Air,
            baseDamage = 8,
            levels = new List<ElementalLevel>
        {
            new ElementalLevel { requiredKills = 0, damageBonus = 0 }, //for test
            new ElementalLevel { requiredKills = 10, damageBonus = 0 },
            new ElementalLevel { requiredKills = 20, damageBonus = 10 },
            new ElementalLevel { requiredKills = 50, damageBonus = 10 },
            new ElementalLevel { requiredKills = 100, damageBonus = 10 }
        }
        };
        
        var earthAtttack = new ElementalAttack
        {
            name = "Earth",
            type = ElementalDamageType.Earth,
            baseDamage = 8,
            levels = new List<ElementalLevel>
        {
            new ElementalLevel { requiredKills = 0, damageBonus = 0 }, //for test
            new ElementalLevel { requiredKills = 10, damageBonus = 0 },
            new ElementalLevel { requiredKills = 20, damageBonus = 10 },
            new ElementalLevel { requiredKills = 50, damageBonus = 10 },
            new ElementalLevel { requiredKills = 100, damageBonus = 10 }
        }
        };
        elementalAttacks.Add(normalAttack);
        elementalAttacks.Add(fireAttack);
        elementalAttacks.Add(waterAttack);
        elementalAttacks.Add(airAtttack);
        elementalAttacks.Add(earthAtttack);
    }

}

