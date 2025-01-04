using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;  // A player referencia
    private Vector2 minBounds;
    private Vector2 maxBounds;
    public float cameraZ = -10f; // A kamera Z tengelyének rögzítése (2D esetén)

    void Start()
    {
        // Automatikusan beállítjuk a kezdõ szoba boundjait
        InitializeBounds();
    }

    void LateUpdate()
    {
        // A player pozíciójának lekérése
        Vector3 targetPosition = player.position;

        // A kamera pozíciójának korlátozása a jelenlegi szoba bounds szerint
        targetPosition.x = Mathf.Clamp(targetPosition.x, minBounds.x, maxBounds.x);
        targetPosition.y = Mathf.Clamp(targetPosition.y, minBounds.y, maxBounds.y);

        // A kamera Z értékének megtartása
        targetPosition.z = cameraZ;

        // A kamera pozíciójának frissítése
        transform.position = targetPosition;
    }

    public void SetBounds(Vector2 min, Vector2 max)
    {
        minBounds = min;
        maxBounds = max;
    }

    private void InitializeBounds()
    {
        // A játékos pozíciójának lekérése alapján ellenõrizzük a szobát
        Collider2D[] colliders = Physics2D.OverlapPointAll(player.position);
        foreach (Collider2D collider in colliders)
        {
            Room room = collider.GetComponentInParent<Room>();  // A szoba keresése a szülõ objektumban
            if (room != null)
            {
                SetBounds(room.minBounds, room.maxBounds);  // A szoba határait beállítjuk
                Debug.Log("Min: " + minBounds + ", Max: " + maxBounds);
                return;
            }
        }
        Debug.LogWarning("No Room found at player start position!");
    }
}
