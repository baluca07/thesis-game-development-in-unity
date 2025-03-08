using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    public int RoomID;

    public Transform minBoundary; 
    public Transform maxBoundary;

    [SerializeField] private CompositeCollider2D floor;

#if UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX || UNITY_STANDALONE_LINUX
    [SerializeField] private DoorLocker[] doors;

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
    public List<GameObject> enemies = new List<GameObject>();

    private PolygonCollider2D playerCollider;

    public bool roomCleared = false;
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) Debug.LogError("Player not found");

        playerCollider = player.GetComponent<PolygonCollider2D>();

        if(playerCollider == null) Debug.LogError("Missing player collider!");
        if (floor.bounds.Intersects(playerCollider.bounds))
        {
            EnterRoom();
        }
    }

    private void EnterRoom()
    {
        GameManager.Instance.currentRoom = this;
        Debug.Log("Player spawned in room: " + gameObject.name);
        GameManager.Instance.EnterRoom();
        UIManager.Instance.UpdateQuestEnemies();
        if (!roomCleared)
        {
#if UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX || UNITY_STANDALONE_LINUX
            LockDoors();
#endif
            SpawnEnemies();
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
        enemies.RemoveAt(enemies.Count - 1);
        UIManager.Instance.UpdateQuestEnemies();
        if (enemies.Count == 0)
        {
#if UNITY_ANDROID || UNITY_IOS
            GameManager.Instance.CompleteLevel(DungeonController.Instance.dungeonID, RoomID);
#elif UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX || UNITY_STANDALONE_LINUX
            OpenDoors();
            UIManager.Instance.UpdateQuestRooms();
#endif
            roomCleared = true;
        }
    }

    private IEnumerator spawnCounter(Transform spawnpoint)
    {
        Instantiate(spawnParticlesPrefab, spawnpoint.position, Quaternion.identity);
        yield return new WaitForSeconds(1f);
        GameObject enemy = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)], spawnpoint.position, Quaternion.identity);
        enemies.Add(enemy);
    }
}
