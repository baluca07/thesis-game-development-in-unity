using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DungeonController : MonoBehaviour
{
    public int dungeonID;
    public static DungeonController Instance;
    [SerializeField] private RoomController[] rooms;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        GameManager.Instance.SpawnPlayer();
        GatherRooms();
        StartCoroutine(SearchPlayer());
        SessionController.Instance.StartSession();
        
#if UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX || UNITY_STANDALONE_LINUX
        roomsCount = rooms.Length;
        UIManager.Instance.UpdateQuestRooms();
#endif
    }
    private void GatherRooms()
    {
        List<RoomController> roomList = new List<RoomController>();

        foreach (Transform child in transform)
        {
            if (child.CompareTag("Room"))
            {
                RoomController roomController = child.GetComponent<RoomController>();
                if (roomController != null)
                {
                    roomList.Add(roomController);
                }
                else
                {
                    Debug.LogWarning("Room object '" + child.name + "' has 'Room' tag but no RoomController component.");
                }
            }
        }
        rooms = roomList.ToArray();
        Debug.Log($"Dungeon{dungeonID} has {rooms.Length} rooms");
    }

    private IEnumerator SearchPlayer()
    {
        yield return new WaitForSeconds(0.5f);
        foreach (RoomController room in rooms)
        {
            room.CheckForPlayer();
        }
    }

#if UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX || UNITY_STANDALONE_LINUX


    public int clearedRooms = 0;
    public int roomsCount = 0;
    
    public void AddRoom()
    {
        clearedRooms++;
        if (clearedRooms == rooms.Length)
        {
            Debug.Log("Dungeon Completed");
            GameManager.Instance.CompleteDungeon(dungeonID);
        }
    }
#endif
}
