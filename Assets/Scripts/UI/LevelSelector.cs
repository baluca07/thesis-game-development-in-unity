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
            if (PlayerPrefs.GetInt("Level" + i + "Completed") == 1)
            {
                Debug.Log($"Level {i} id completed, button set active");
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

#if UNITY_ANDROID || UNITY_IOS
    public void BackToDungeons()
    {
        SceneManager.LoadScene("LevelSelector");
    }
    public void LoadLevel(string parameters)
    {
        string[] parts = parameters.Split(',');
        int dungeonIndex = int.Parse(parts[0]);
        int levelIndex = int.Parse(parts[1]);
        GameManager.Instance.SetSpawnpoint(dungeonIndex, levelIndex);
        SceneManager.LoadScene("Dungeon" + dungeonIndex);
    }
#endif

}
