using UnityEngine;

public class Room : MonoBehaviour
{
    [HideInInspector] public Vector2 minBounds;
    [HideInInspector] public Vector2 maxBounds;

    [Tooltip("Vertical scale factor to reduce the room size vertically.")]
    [Range(0.1f, 1f)] public float verticalScaleFactor = 0.5f;  // Vertikális méret csökkentése

    [Tooltip("Horizontal scale factor to reduce the room size horizontally.")]
    [Range(0.1f, 1f)] public float horizontalScaleFactor = 0.5f;  // Horizontális méret csökkentése

    private void Awake()
    {
        CalculateBounds();
    }

    public void CalculateBounds()
    {
        // Lekérjük a collider-t
        Collider2D roomCollider = GetComponentInChildren<Collider2D>();

        if (roomCollider != null)
        {
            // A collider határainak lekérdezése
            Bounds bounds = roomCollider.bounds;

            minBounds = bounds.min;
            maxBounds = bounds.max;

            // A szoba középpontja
            Vector2 center = (minBounds + maxBounds) / 2;

            // A szoba méretének módosítása a scale faktorokkal
            Vector2 size = maxBounds - minBounds;
            size.x *= horizontalScaleFactor;  // Horizontális méret csökkentése
            size.y *= verticalScaleFactor;    // Vertikális méret csökkentése

            // Az új határok számítása a scale faktorok alapján
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
        // Csak a szerkesztõ módban jelenítjük meg a bound-okat
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(minBounds.x, minBounds.y, 0), new Vector3(maxBounds.x, minBounds.y, 0));
        Gizmos.DrawLine(new Vector3(maxBounds.x, minBounds.y, 0), new Vector3(maxBounds.x, maxBounds.y, 0));
        Gizmos.DrawLine(new Vector3(maxBounds.x, maxBounds.y, 0), new Vector3(minBounds.x, maxBounds.y, 0));
        Gizmos.DrawLine(new Vector3(minBounds.x, maxBounds.y, 0), new Vector3(minBounds.x, minBounds.y, 0));
    }
}

