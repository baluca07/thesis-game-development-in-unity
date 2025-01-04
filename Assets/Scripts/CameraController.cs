using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player; // A player referencia
    private Vector2 minBounds;
    private Vector2 maxBounds;
    public float cameraZ = -10f; // A kamera Z tengely�nek r�gz�t�se (2D eset�n)

    void Start()
    {
        // Automatikusan be�ll�tjuk a kezd� szoba boundjait
        InitializeBounds();
    }

    void LateUpdate()
    {
        // A player poz�ci�j�nak lek�r�se
        Vector3 targetPosition = player.position;

        // A kamera poz�ci�j�nak korl�toz�sa a jelenlegi szoba bounds szerint
        targetPosition.x = Mathf.Clamp(targetPosition.x, minBounds.x, maxBounds.x);
        targetPosition.y = Mathf.Clamp(targetPosition.y, minBounds.y, maxBounds.y);

        // A kamera Z �rt�k�nek megtart�sa
        targetPosition.z = cameraZ;

        // A kamera poz�ci�j�nak friss�t�se
        transform.position = targetPosition;
    }

    public void SetBounds(Vector2 min, Vector2 max)
    {
        minBounds = min;
        maxBounds = max;
    }

    private void InitializeBounds()
    {
        Collider2D[] colliders = Physics2D.OverlapPointAll(player.position);
        foreach (Collider2D collider in colliders)
        {
            Room room = collider.GetComponent<Room>();
            if (room != null)
            {
                SetBounds(room.minBounds, room.maxBounds);
               Debug.Log("Min: " + minBounds + "Max: " + maxBounds);
                return;
            }
        }
        Debug.LogWarning("No Room found at player start position!");
    }
}

