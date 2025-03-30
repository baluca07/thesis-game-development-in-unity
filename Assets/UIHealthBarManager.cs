using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthBarManager : MonoBehaviour
{
    [SerializeField] private Slider healthFill;

    private void Start()
    {
        healthFill = GetComponent<Slider>();
        healthFill.minValue = 0;
        healthFill.maxValue = PlayerStats.Instance.maxHealth;
        healthFill.value = PlayerStats.Instance.maxHealth;
    }
    public void UpdatePlayerHealthFill()
    {
        healthFill.value = PlayerStats.Instance.currentHealth;
    }
}
