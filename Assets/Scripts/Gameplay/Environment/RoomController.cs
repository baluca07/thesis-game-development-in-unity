using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    public Transform minBoundary; 
    public Transform maxBoundary;

    [SerializeField] private CompositeCollider2D floor;

    [SerializeField] private DoorLocker[] doors;

    public int RoomID;

    public int enemyCount;

    private PolygonCollider2D playerCollider;

    public Transform[] enemySpawnpoints;

    public GameObject[] enemyPrefabs;

    public ParticleSystem spawnParticlesPrefab;

    public bool roomCleared = false;
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) Debug.LogError("Player not found");

        playerCollider = player.GetComponent<PolygonCollider2D>();

        if(playerCollider == null) Debug.LogError("Missing player collider!");
        if (floor.bounds.Intersects(playerCollider.bounds))
        {
            GameManager.Instance.currentRoom = this;
            Debug.Log("Player spawned in room: " + gameObject.name);
            if (!roomCleared) { 
                LockDoors();
                //SpawnEnemies();
            }
        }
    }

    private void Update()
    {
        if (enemyCount == 0) { 
            OpenDoors();
            roomCleared = true;
        }
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
    private void SpawnEnemies()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            Transform spawnpoint = enemySpawnpoints[Random.Range(0, enemySpawnpoints.Length)];
            
            StartCoroutine(spawnCounter(spawnpoint));
            
        }
    }

    private IEnumerator spawnCounter(Transform spawnpoint)
    {
        Instantiate(spawnParticlesPrefab, spawnpoint.position, Quaternion.identity);
        yield return new WaitForSeconds(1f);
        Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)], spawnpoint.position, Quaternion.identity);
    }
}
