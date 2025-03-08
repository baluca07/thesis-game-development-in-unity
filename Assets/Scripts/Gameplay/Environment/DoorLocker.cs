using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DoorLocker : MonoBehaviour
{
    [SerializeField] private Collider2D exitCollider;
    [SerializeField] private Light2D doorLight;
    [SerializeField] private Color lockColor;
    [SerializeField] private Color openColor;
    public void LockDoor()
    {
        exitCollider.enabled = false;
        doorLight.color = lockColor;
    }

    public void OpenDoor() {
        exitCollider.enabled = true;
        doorLight.color = openColor;
    }
}
