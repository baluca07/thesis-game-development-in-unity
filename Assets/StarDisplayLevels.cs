using UnityEngine;
using UnityEngine.UI;

public class StarDisplayLevels : MonoBehaviour
{
    public Image[] starImages;
    public Sprite filledStar;
    public Sprite emptyStar;

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
}