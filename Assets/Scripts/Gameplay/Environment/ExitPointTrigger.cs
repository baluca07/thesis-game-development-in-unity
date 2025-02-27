using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitPointTrigger : MonoBehaviour
{
    [Tooltip("The spawn point where the player should be moved when entering this door.")]
    public Transform otherRoomSpawnPoint;
    public RoomBoundaryTrigger targetRoom;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {      
            GameManager.Instance.MovePlayerToRoom(other.transform, otherRoomSpawnPoint);
            targetRoom.UpdateBoundariesToCurrent();
        }
    }
}


