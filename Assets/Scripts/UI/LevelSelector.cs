using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    public CanBeDisabledButton[] levelButtons;
    void Start()
    {
        UpdateButtons();
    }

    void UpdateButtons()
    {
        for (int i = 2; i < levelButtons.Length + 2; i++)
        {
            if (PlayerPrefs.GetInt("Dungeon" + (i - 1) + "Completed") == 1)
            {
                Debug.Log($"Level {(i - 1)} id completed, following button set active");
                levelButtons[i-2].SetInteractable();
            }
            else
            {
                levelButtons[i-2].SetDisabled();
            }
        }
    }

    public void LoadDungeon(int dungeonIndex)
    {
 #if UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX || UNITY_STANDALONE_LINUX
        SceneManager.LoadScene("Dungeon" + dungeonIndex);
#else
        SceneManager.LoadScene("LevelsOfDungeon" + dungeonIndex);
#endif         
    }

}
