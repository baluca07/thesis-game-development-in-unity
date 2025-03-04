using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButtons : MonoBehaviour
{
    public void Replay()
    {
        GameManager.Instance.ResetGame();
    }
    public void DummyButtonCheck()
    {
        Debug.Log("Button clicked");
    }
}
