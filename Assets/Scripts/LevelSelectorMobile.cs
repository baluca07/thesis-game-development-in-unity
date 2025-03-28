using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectorMoblie : MonoBehaviour
{
#if UNITY_ANDROID || UNITY_IOS
    public int dungeonIndex;
    public CanBeDisabledButton[] levelButtons;
    public StarDisplayLevels[] stars;
    void Start()
    {
        UpdateStarts();
        UpdateButtons();
    }

    void UpdateButtons()
    {
        for (int i = 2; i < levelButtons.Length + 2; i++)
        {
            if (PlayerPrefs.GetInt("Dungeon" + dungeonIndex + "Level" + (i - 1) + "Completed") == 1)
            {
                Debug.Log($"Level {i} id completed, following button set active");
                levelButtons[i - 2].SetInteractable();
            }
            else
            {
                levelButtons[i - 2].SetDisabled();
            }
        }
    }
    void UpdateStarts()
    {
        for (int i = 0; i < stars.Length; i++)
        {
            Debug.Log(PlayerPrefs.GetInt("Dungeon" + dungeonIndex + "Level" + (i + 1) + "Stars"));
            int startsToDisplay = PlayerPrefs.GetInt("Dungeon" + dungeonIndex + "Level" + (i + 1) + "Stars");
            stars[i].DisplayStars(startsToDisplay);
        }
    }
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