using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanBeDisabledButton : MonoBehaviour
{
    public float disabledAlpha = 0.7f;

    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI buttonText;

    [SerializeField] private bool iteractableAtStart = true;
    private void Awake()
    {
        button = GetComponent<Button>();

        if (button == null) Debug.Log("Missing button!");
        if (buttonText == null) Debug.Log("Missing button-text!");

        if (iteractableAtStart)
        {
            SetInteractable();
        }
        else
        {
            SetDisabled();
        }
    }

    public void SetDisabled()
    {
        button.interactable = false;
        buttonText.alpha = disabledAlpha;
    }
    public void SetInteractable()
    {
        button.interactable = true;
        buttonText.alpha = 1;
    }
}
