using UnityEngine;
using UnityEngine.U2D.Animation;
using UnityEngine.UI;

public class UISpriteResolver : MonoBehaviour
{
    [SerializeField] private SpriteResolver spriteResolver;
    [SerializeField] private Image image;
    [SerializeField] private SpriteRenderer currentSprite;

    private void Start()
    {
        spriteResolver = GetComponent<SpriteResolver>();
        currentSprite = GetComponent<SpriteRenderer>();
        image = GetComponent<Image>();
    }

    public void UpdateSprite(string label)
    {
        spriteResolver.SetCategoryAndLabel("elementIcon", label);
        if (currentSprite != null && image != null)
        {
            image.sprite = currentSprite.sprite;
            image.SetNativeSize();
        }
    }
}