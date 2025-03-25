using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitPointTrigger : MonoBehaviour
{
    public Transform otherRoomSpawnPoint;
    public int targetRoomID;
    [SerializeField] private RoomController targetRoom;

    private void Start()
    {
        string roomName = "Room" + targetRoomID;
        GameObject foundRoom = GameObject.Find(roomName);

        if (foundRoom != null)
        {
            targetRoom = foundRoom.GetComponent<RoomController>();
        }
        else
        {
            Debug.LogWarning("GameObject with name '" + roomName + "' not found in the scene.");
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {      
            GameManager.Instance.MovePlayerToRoomAndSpawnEnemies(other.transform, otherRoomSpawnPoint, targetRoom);
            Debug.Log($"Player moved to Room{targetRoomID}");
        }
    }
}


