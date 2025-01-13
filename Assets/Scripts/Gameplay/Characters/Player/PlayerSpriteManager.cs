using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class PlayerSpriteManager : MonoBehaviour
{
    [SerializeField] GameObject frontHand;
    [SerializeField] GameObject backHand;
    [SerializeField] GameObject face;

    public void ChangeFrontHandSprite(string newLabel)
    {
        var resolver = frontHand.GetComponent<SpriteResolver>();
        if (resolver != null)
        {
            resolver.SetCategoryAndLabel(resolver.GetCategory(), newLabel);
        }
    }

    public void ChangeBackHandSprite(string newLabel)
    {
        var resolver = backHand.GetComponent<SpriteResolver>();
        if (resolver != null)
        {
            resolver.SetCategoryAndLabel(resolver.GetCategory(), newLabel);
        }
    }

    public void ChangeFaceSprite(string newLabel)
    {
        var resolver = face.GetComponent<SpriteResolver>();
        if (resolver != null)
        {
            resolver.SetCategoryAndLabel(resolver.GetCategory(), newLabel);
        }
    }
}

