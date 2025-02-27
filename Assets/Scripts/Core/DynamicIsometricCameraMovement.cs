using TMPro;
using UnityEngine;

public class DynamicIsometricCameraFollow : MonoBehaviour
{
    public Transform player;
    public float smoothSpeed = 0.125f;
    public float cameraZ = -10f;

    private Vector2 boundaryMin;
    private Vector2 boundaryMax;

    [SerializeField] private Transform startRoomMin;
    [SerializeField] private Transform startRoomMax;

    void Start()
    {
        // Initialize first 
        UpdateBoundaries(new Vector2(startRoomMin.position.x, startRoomMin.position.y), new Vector2(startRoomMax.position.x, startRoomMax.position.y)); // Példa értékek, ezt módosítsd a saját szobádhoz
    }

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
    public void UpdateBoundaries(Vector2 newMin, Vector2 newMax)
    {
        boundaryMin = newMin;
        boundaryMax = newMax;
    }
}