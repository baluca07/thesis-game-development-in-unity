using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitPointTrigger : MonoBehaviour
{
    public Transform otherRoomSpawnPoint;
    public RoomController targetRoom;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {      
            GameManager.Instance.MovePlayerToRoom(other.transform, otherRoomSpawnPoint);
            Debug.Log($"Player moved to Room{targetRoom.roomID}");
        }
    }
}


