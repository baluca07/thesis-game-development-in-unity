using UnityEngine;

public class RoomBoundaryTrigger : MonoBehaviour
{
    private Vector2 roomBoundaryMin;
    private Vector2 roomBoundaryMax;

    public Transform minTransform;
    public Transform maxTransform;

    private DynamicCameraFollow cameraController;

    void Start()
    {
        roomBoundaryMin = minTransform.position;
        roomBoundaryMax = maxTransform.position;

        cameraController = Camera.main.GetComponent<DynamicCameraFollow>();
        if (cameraController == null)
        {
            Debug.LogError("DynamicCameraFollow script not found on the main camera.");
        }
    }
    public void UpdateBoundariesToCurrent()
    {
        cameraController.UpdateBoundaries();
    }
}
