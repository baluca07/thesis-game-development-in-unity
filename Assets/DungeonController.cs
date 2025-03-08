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
        roomsCount = rooms.Length;
        UIManager.Instance.UpdateQuestRooms();
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
