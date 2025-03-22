using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;
using UnityEngine.UI;

public class UISpriteResolverMobile : MonoBehaviour
{
    [SerializeField] private SpriteResolver spriteResolver;
    [SerializeField] private Image image;
    [SerializeField] private SpriteRenderer currentSprite;
    public string elementalType;

    private void Start()
    {
        spriteResolver = GetComponent<SpriteResolver>();
        currentSprite = GetComponent<SpriteRenderer>();
        image = GetComponent<Image>();
    }

    private void Update()
    {
        foreach(var elementalAttack in GameManager.Instance.elementalAttacks)
        {
            if(elementalAttack.name == elementalType)
            {
                if (elementalAttack.currentLevel == 0)
                {
                    Deactivate();
                }
                else
                {
                    Activate();
                    Destroy(this);
                }
            }
        }
    }

    public void Activate()
    {
        spriteResolver.SetCategoryAndLabel("elementIcon", elementalType);
        if (currentSprite != null && image != null)
        {
            image.sprite = currentSprite.sprite;
            image.SetNativeSize();
        }
    }
    public void Deactivate()
    {
        string fullLabel = elementalType+"Disabled";
        spriteResolver.SetCategoryAndLabel("elementIcon", fullLabel);
        if (currentSprite != null && image != null)
        {
            image.sprite = currentSprite.sprite;
            image.SetNativeSize();
        }
    }
}
