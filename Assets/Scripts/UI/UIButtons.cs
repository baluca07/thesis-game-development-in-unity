using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIButtons : MonoBehaviour
{
    public void Replay()
    {
        GameManager.Instance.ResetLevel();
    }
    public void DummyButtonCheck()
    {
        Debug.Log("Button clicked");
    }

    public void ContinueGame()
    {
        GameManager.Instance.ContinueGame();
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void GoBackToLevels()
    {
#if UNITY_ANDROID || UNITY_IOS
        SceneManager.LoadScene("LevelsOfDungeon" + DungeonController.Instance.dungeonID);
#elif UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX || UNITY_STANDALONE_LINUX
        SceneManager.LoadScene("LevelSelector");
#endif
    }
}
