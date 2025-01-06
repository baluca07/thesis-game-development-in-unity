using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
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
}

