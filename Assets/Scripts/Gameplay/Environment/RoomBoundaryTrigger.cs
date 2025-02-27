using UnityEngine;

public class RoomBoundaryTrigger : MonoBehaviour
{
    private Vector2 roomBoundaryMin;
    private Vector2 roomBoundaryMax;

    public Transform minTransform;
    public Transform maxTransform;

    private DynamicIsometricCameraFollow cameraController;

    void Start()
    {
        roomBoundaryMin = minTransform.position;
        roomBoundaryMax = maxTransform.position;

        cameraController = Camera.main.GetComponent<DynamicIsometricCameraFollow>();
        if (cameraController == null)
        {
            Debug.LogError("DynamicIsometricCameraFollow script not found on the main camera.");
        }
    }
    public void UpdateBoundariesToCurrent()
    {
        cameraController.UpdateBoundaries(roomBoundaryMin, roomBoundaryMax);
    }
}
