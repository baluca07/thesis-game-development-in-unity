using UnityEngine;

public class Room : MonoBehaviour
{
    [HideInInspector] public Vector2 minBounds;
    [HideInInspector] public Vector2 maxBounds;

    [Tooltip("Vertical scale factor to reduce the room size vertically.")]
    [Range(0.1f, 1f)] public float verticalScaleFactor = 0.5f;  // Vertik�lis m�ret cs�kkent�se

    [Tooltip("Horizontal scale factor to reduce the room size horizontally.")]
    [Range(0.1f, 1f)] public float horizontalScaleFactor = 0.5f;  // Horizont�lis m�ret cs�kkent�se

    private void Awake()
    {
        CalculateBounds();
    }

    public void CalculateBounds()
    {
        // Lek�rj�k a collider-t
        Collider2D roomCollider = GetComponentInChildren<Collider2D>();

        if (roomCollider != null)
        {
            // A collider hat�rainak lek�rdez�se
            Bounds bounds = roomCollider.bounds;

            minBounds = bounds.min;
            maxBounds = bounds.max;

            // A szoba k�z�ppontja
            Vector2 center = (minBounds + maxBounds) / 2;

            // A szoba m�ret�nek m�dos�t�sa a scale faktorokkal
            Vector2 size = maxBounds - minBounds;
            size.x *= horizontalScaleFactor;  // Horizont�lis m�ret cs�kkent�se
            size.y *= verticalScaleFactor;    // Vertik�lis m�ret cs�kkent�se

            // Az �j hat�rok sz�m�t�sa a scale faktorok alapj�n
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
        // Csak a szerkeszt� m�dban jelen�tj�k meg a bound-okat
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(minBounds.x, minBounds.y, 0), new Vector3(maxBounds.x, minBounds.y, 0));
        Gizmos.DrawLine(new Vector3(maxBounds.x, minBounds.y, 0), new Vector3(maxBounds.x, maxBounds.y, 0));
        Gizmos.DrawLine(new Vector3(maxBounds.x, maxBounds.y, 0), new Vector3(minBounds.x, maxBounds.y, 0));
        Gizmos.DrawLine(new Vector3(minBounds.x, maxBounds.y, 0), new Vector3(minBounds.x, minBounds.y, 0));
    }
}

