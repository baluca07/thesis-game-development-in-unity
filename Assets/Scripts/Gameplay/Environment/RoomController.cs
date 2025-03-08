using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    public Transform minBoundary; 
    public Transform maxBoundary;

    [SerializeField] private CompositeCollider2D floor;

    private PolygonCollider2D playerCollider;
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
        }
    }

}
