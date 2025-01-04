using UnityEngine;

public class Room : MonoBehaviour
{
    public Vector2 minBounds;
    public Vector2 maxBounds;

    [SerializeField] GameObject lowerLeft;
    [SerializeField] GameObject upperRight;

    private void Awake()
    {
        minBounds = lowerLeft.transform.position;
        Debug.Log("MinBounds: " + minBounds);
        maxBounds = upperRight.transform.position;
        Debug.Log("MaxBounds: " + maxBounds);
    }


}

