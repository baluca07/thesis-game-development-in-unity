using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelector : MonoBehaviour
{
    public CanBeDisabledButton[] levelButtons;
    void Start()
    {
        UpdateButtons();
    }

    void UpdateButtons()
    {
        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (i == 0 || PlayerPrefs.GetInt("Level" + i + "Completed") == 1)
            {
                levelButtons[i].SetInteractable();
            }
            else
            {
                levelButtons[i].SetDisabled();
            }
        }
    }

    public void CompleteLevel(int levelIndex)
    {
        PlayerPrefs.SetInt("Level" + levelIndex + "Completed", 1);
        UpdateButtons();
    }
}
