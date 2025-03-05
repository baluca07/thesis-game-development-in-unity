using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanBeDisabledButtons : MonoBehaviour
{
    public float disabledAlpha = 0.7f;

    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI buttonText;

    [SerializeField] private bool iteractableAtStart = true;
    private void Start()
    {
        button = GetComponent<Button>();

        if (iteractableAtStart)
        {
            SetActive();
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
    public void SetActive()
    {
        button.interactable = true;
        buttonText.alpha = 1;
    }
}
