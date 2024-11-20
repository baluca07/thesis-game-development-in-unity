using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DoorTrigger : MonoBehaviour
{
    [Tooltip("The spawn point where the player should be moved when entering this door.")]
    public Transform spawnPoint;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.MovePlayerToRoom(other.transform, spawnPoint);
        }
    }
}

