using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    public int roomID;

    public Transform minBoundary; 
    public Transform maxBoundary;

    [SerializeField] private CompositeCollider2D floor;

#if UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX || UNITY_STANDALONE_LINUX
    [SerializeField] private DoorLocker[] doors;

    private void GatherDoors()
    {
        List<DoorLocker> doorList = new List<DoorLocker>();

        foreach (Transform child in transform)
        {
            foreach(Transform obj in child)
            {
                if (obj.CompareTag("Door"))
                {
                    DoorLocker doorLocker = obj.GetComponent<DoorLocker>();
                    if (doorLocker != null)
                    {
                        doorList.Add(doorLocker);
                    }
                    else
                    {
                        Debug.LogWarning("Door object '" + obj.name + "' has 'Door' tag but no DoorLocker component.");
                    }
                }
            }
        }

        doors = doorList.ToArray();
        Debug.Log($"Room{roomID} has {doors.Length} doors");
    }

    public void LockDoors()
    {
        foreach (DoorLocker door in doors)
        {
            door.LockDoor();
        }
        Debug.Log("Doors Locked");
    }

    public void OpenDoors()
    {
        foreach (DoorLocker door in doors)
        {
            door.OpenDoor();
        }
        Debug.Log("Doors Open");
    }

#endif

    public int enemyCount;
    public Transform[] enemySpawnpoints;
    public GameObject[] enemyPrefabs;
    public ParticleSystem spawnParticlesPrefab;
    public int currentEnemies;

    private PolygonCollider2D playerCollider;

    public bool roomCleared = false;
    void Start()
    {
#if UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX || UNITY_STANDALONE_LINUX
        GatherDoors();
#endif
        //CheckForPlayer();
        
    }

    public void CheckForPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) Debug.LogError("Player not found");
        playerCollider = player.GetComponent<PolygonCollider2D>();

        if (playerCollider == null) Debug.LogError("Missing player collider!");
        if (floor.bounds.Intersects(playerCollider.bounds))
        {
            GameManager.Instance.currentRoom = this;
            Debug.Log("Player spawned in room: " + gameObject.name);
            GameManager.Instance.EnterRoom();
#if UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX || UNITY_STANDALONE_LINUX
            if (!roomCleared)
            {
                LockDoors();
                SpawnEnemies();
                UIManager.Instance.UpdateQuestEnemies(enemyCount, 0);
            }
#elif UNITY_ANDROID || UNITY_IOS
            SpawnEnemies();
            UIManager.Instance.UpdateQuestEnemies(enemyCount, 0);
#endif

        }
    }

    private void SpawnEnemies()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            Transform spawnpoint = enemySpawnpoints[Random.Range(0, enemySpawnpoints.Length)];
            
            StartCoroutine(spawnCounter(spawnpoint));

        }
    }
    public void EnemyDefeated()
    {
        currentEnemies--;
        UIManager.Instance.UpdateQuestEnemies(enemyCount,currentEnemies);
        if (currentEnemies == 0)
        {
#if UNITY_ANDROID || UNITY_IOS
            GameManager.Instance.CompleteLevel(DungeonController.Instance.dungeonID, roomID);
            GameManager.Instance.Win();
#elif UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX || UNITY_STANDALONE_LINUX
            OpenDoors();
            DungeonController.Instance.AddRoom();
            UIManager.Instance.UpdateQuestRooms();
            roomCleared = true;
#endif
        }
    }

    private IEnumerator spawnCounter(Transform spawnpoint)
    {
        Instantiate(spawnParticlesPrefab, spawnpoint.position, Quaternion.identity);
        yield return new WaitForSeconds(1f);
        Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)], spawnpoint.position, Quaternion.identity);
        currentEnemies++;
    }

}
