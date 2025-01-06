using UnityEngine;
using UnityEngine.InputSystem;

public class AimRangedAttack : MonoBehaviour
{
    private bool isAim = false;

    void Update()
    {
        if (!isAim) return;

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        Vector2 direction = (mousePosition - transform.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        Debug.DrawLine(transform.position, mousePosition, Color.red);
    }

    public void OnAim(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isAim = true;
            Debug.Log("Aim started");
        }
        else if (context.canceled)
        {
            isAim = false;
            Debug.Log("Aim canceled");
        }
    }
}

