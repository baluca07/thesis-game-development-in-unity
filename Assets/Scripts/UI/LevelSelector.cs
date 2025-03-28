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
        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (PlayerPrefs.GetInt("Dungeon" + ( i + 1 ) + "Completed") == 1)
            {
                Debug.Log($"Level {(i + 1)} id completed, following button set active");
                levelButtons[i].SetInteractable();
            }
            else
            {
                levelButtons[i].SetDisabled();
            }
        }
    }

    public void LoadDungeon(int dungeonIndex)
    {
#if UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX || UNITY_STANDALONE_LINUX
        SceneManager.LoadScene("Dungeon" + dungeonIndex);
#elif UNITY_ANDROID || UNITY_IOS
        SceneManager.LoadScene("LevelsOfDungeon" + dungeonIndex);
#endif         
    }

}
