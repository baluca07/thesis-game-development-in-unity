using UnityEngine;
using UnityEngine.InputSystem;

public class Aim : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject weaponObject;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        weaponObject = transform.parent.gameObject;

    }
    void Update()
    {

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        Vector2 direction = (mousePosition - transform.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, angle);
        weaponObject.transform.rotation = transform.rotation;
    }
}
