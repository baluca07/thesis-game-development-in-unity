using UnityEngine;
using UnityEngine.InputSystem;

public class Aim : MonoBehaviour
{
    void Update()
    {

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        Vector2 direction = (mousePosition - transform.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Debug.Log(angle);
        
        /*float characterFacingYRotation = player.transform.rotation.eulerAngles.y;
        if (characterFacingYRotation == 0)
        {
            angle = Mathf.Clamp(angle, -90f, 90f);
        }
        else if (Mathf.Approximately(characterFacingYRotation, 180f))
        {
            if (angle > 0)
                angle = Mathf.Clamp(angle, 90f, 180f);
            else
                angle = Mathf.Clamp(angle, -180f, -90f);
        }*/

        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
