using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMainMenu : MonoBehaviour
{
    public CanBeDisabledButton loadButton;
    public string playerPrefsKey = "SavedGame"; //Save with PlayerPrefs.SetString("SavedGame",date)

    void Start()
    {
        if (PlayerPrefs.HasKey(playerPrefsKey))
        {
            loadButton.SetInteractable();
        }
        else
        {
            loadButton.SetDisabled();
        }
    }

    public void ContinuePreviousGame()
    {
        Debug.Log("Continue Previous Game");
        GameManager.Instance.InitializeElementalAttacks(PlayerPrefs.GetInt("NormalAttack"),
                                                        PlayerPrefs.GetInt("FireAttack"),
                                                        PlayerPrefs.GetInt("WaterAttack"),
                                                        PlayerPrefs.GetInt("AirAttack"),
                                                        PlayerPrefs.GetInt("EarthAttack"));
        SceneManager.LoadScene("LevelSelector");
    }

    public void NewGame()
    {
        Debug.Log("Start a new game");
        ResetSave();
        GameManager.Instance.InitializeElementalAttacks(0, 0, 0, 0, 0);
        SceneManager.LoadScene("LevelSelector");
    }

    public void ExitGame()
    {
        Debug.Log("Saving data...");
        PlayerPrefs.Save();
        Debug.Log("Exiting game...");
        Application.Quit();
    }
    private void ResetSave()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        if (PlayerPrefs.HasKey(playerPrefsKey)) 
        { 
            Debug.Log("PlayerPref isn't cleared !"); 
        }
        else
        {
             Debug.Log("PlayerPrefs data cleared");
        }
    }
}
