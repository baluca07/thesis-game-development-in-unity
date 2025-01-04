using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [Tooltip("The spawn point where the player should be moved when entering this door.")]
    public Transform spawnPoint;

    [Tooltip("The target room associated with this door.")]
    public Room targetRoom;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
           
            GameManager.Instance.MovePlayerToRoom(other.transform, spawnPoint);

            CameraController cameraController = FindObjectOfType<CameraController>();
            if (cameraController != null && targetRoom != null)
            {
                cameraController.SetBounds(targetRoom.minBounds, targetRoom.maxBounds);
                Debug.Log("Switched to room: " + targetRoom.name);
            }
            else
            {
                Debug.LogError("CameraController or TargetRoom is missing!");
            }
        }
    }
}


