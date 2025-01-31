using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    private Vector2 minBounds;
    private Vector2 maxBounds;
    public float cameraZ = -10f;

    void Start()
    {
        player = PlayerStats.Instance.transform;
        InitializeCameraBounds();
    }

    void LateUpdate()
    {
        Vector3 targetPosition = player.position;

        targetPosition.x = Mathf.Clamp(targetPosition.x, minBounds.x, maxBounds.x);
        targetPosition.y = Mathf.Clamp(targetPosition.y, minBounds.y, maxBounds.y);

        targetPosition.z = cameraZ;

        transform.position = targetPosition;
    }

    public void SetBounds(Vector2 min, Vector2 max)
    {
        minBounds = min;
        maxBounds = max;
    }

    private void InitializeCameraBounds()
    {
        Collider2D[] colliders = Physics2D.OverlapPointAll(player.position);
        foreach (Collider2D collider in colliders)
        {
            Room room = collider.GetComponentInParent<Room>();
            if (room != null)
            {
                SetBounds(room.minBounds, room.maxBounds);
                //Debug.Log("Min: " + minBounds + ", Max: " + maxBounds);
                return;
            }
        }
        Debug.LogWarning("No Room found at player start position!");
    }
}
