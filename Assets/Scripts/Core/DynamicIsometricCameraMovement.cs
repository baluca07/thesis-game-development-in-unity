using TMPro;
using UnityEngine;

public class DynamicIsometricCameraFollow : MonoBehaviour
{
    public Transform player;
    public float smoothSpeed = 0.125f;
    public float cameraZ = -10f;

    private Vector2 boundaryMin;
    private Vector2 boundaryMax;

    void LateUpdate()
    {
        if (player == null)
        {
            Debug.LogWarning("Player reference is not set in DynamicIsometricCameraFollow script.");
            return;
        }

        Vector3 desiredPosition = player.position;

        desiredPosition.x = Mathf.Clamp(desiredPosition.x, boundaryMin.x, boundaryMax.x);
        desiredPosition.y = Mathf.Clamp(desiredPosition.y, boundaryMin.y, boundaryMax.y);
        desiredPosition.z = cameraZ;

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

    }
    public void UpdateBoundaries()
    {
        Debug.Log($"Updated camera boundaries: {GameManager.Instance.currentRoom.name}");
        boundaryMin = new Vector2(GameManager.Instance.currentRoom.minBoundary.position.x,
                                    GameManager.Instance.currentRoom.minBoundary.position.y);
        boundaryMax = new Vector2(GameManager.Instance.currentRoom.maxBoundary.position.x,
                                    GameManager.Instance.currentRoom.maxBoundary.position.y);
    }
}