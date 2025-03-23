using UnityEngine;
using UnityEngine.UI;

public class StarDisplay : MonoBehaviour
{
    public Image[] starImages;
    public Sprite filledStar;
    public Sprite emptyStar;

    public static StarDisplay Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else
        {
            Destroy(gameObject);
        }
    }
    public void DisplayStars(int numberOfStars)
    {
        for (int i = 0; i < starImages.Length; i++)
        {
            if (i < numberOfStars)
            {
                starImages[i].sprite = filledStar;
                starImages[i].SetNativeSize();
            }
            else
            {
                starImages[i].sprite = emptyStar;
                starImages[i].SetNativeSize();
            }
        }
    }
    public int CalculateStars(int score)
    {
        if (score <= GameManager.Instance.currentRoom.StarScores[0])
        {
            return 1;
        }
        else if (score >= GameManager.Instance.currentRoom.StarScores[1])
        {
            return 3;
        }
        else
        {
            return 2;
        }
    }
}