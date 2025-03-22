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
            }
            else
            {
                starImages[i].sprite = emptyStar;
            }
        }
    }
    public int CalculateStars(int score)
    {
        if (score >= 665)
        {
            return 2;
        }
        else if (score >= 1330)
        {
            return 3;
        }
        else
        {
            return 1;
        }
    }
}