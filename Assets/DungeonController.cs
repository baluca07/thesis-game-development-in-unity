using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonController : MonoBehaviour
{
    public int dungeonID;

    [SerializeField] private RoomController[] rooms;

    public int clearedRooms = 0;
    public int roomsCount = 0;

    public static DungeonController Instance;

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
        GatherRooms();
        roomsCount = rooms.Length;
        UIManager.Instance.UpdateQuestRooms();
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


    public void AddRoom()
    {
        clearedRooms++;
        if (clearedRooms == rooms.Length)
        {
            Debug.Log("Dungeon Completed");
            GameManager.Instance.CompleteDungeon(dungeonID);
            UIManager.Instance.ActivateWinScreen();
        }
    }
}
