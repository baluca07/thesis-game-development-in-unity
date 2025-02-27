using UnityEngine;

public class Room : MonoBehaviour
{
    [HideInInspector] public Vector2 minBounds;
    [HideInInspector] public Vector2 maxBounds;

    [Tooltip("Vertical scale factor to reduce the room size vertically.")]
    [Range(0.1f, 1f)] public float verticalScaleFactor = 0.5f;

    [Tooltip("Horizontal scale factor to reduce the room size horizontally.")]
    [Range(0.1f, 1f)] public float horizontalScaleFactor = 0.5f;

    private void Awake()
    {
        CalculateBounds();
    }

    //private void Update()
    //{
    //    OnDrawGizmosSelected();
    //}
    public void CalculateBounds()
    {
        Collider2D roomCollider = GetComponentInChildren<Collider2D>();

        if (roomCollider != null)
        {
            Bounds bounds = roomCollider.bounds;

            minBounds = bounds.min;
            maxBounds = bounds.max;

            Vector2 center = (minBounds + maxBounds) / 2;

            Vector2 size = maxBounds - minBounds;
            size.x *= horizontalScaleFactor;
            size.y *= verticalScaleFactor;

           
            minBounds = center - size / 2;
            maxBounds = center + size / 2;

            Debug.Log("Bounds calculated for room " + name + ": Min = " + minBounds + ", Max = " + maxBounds);
        }
        else
        {
            Debug.LogError("No Collider2D found on the Room object!");
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(minBounds.x, minBounds.y, 0), new Vector3(maxBounds.x, minBounds.y, 0));
        Gizmos.DrawLine(new Vector3(maxBounds.x, minBounds.y, 0), new Vector3(maxBounds.x, maxBounds.y, 0));
        Gizmos.DrawLine(new Vector3(maxBounds.x, maxBounds.y, 0), new Vector3(minBounds.x, maxBounds.y, 0));
        Gizmos.DrawLine(new Vector3(minBounds.x, maxBounds.y, 0), new Vector3(minBounds.x, minBounds.y, 0));
    }
}

