using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SpriteHelper : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteA;
    [SerializeField] SpriteRenderer spriteB;
    public SpriteRenderer spriteNow;
    [SerializeField] SpriteRenderer spriteLast;
    [SerializeField] bool isSideA = true;

    void Awake()
    {
        isSideA = true;
        spriteNow = spriteA;
        spriteLast = spriteB;
    }

    public void OnChangeSprite(Sprite sprite)
    {
        SideChange();
        spriteNow.sprite = sprite;
        spriteNow.DOFade(1,  StageController.actionTime);
        spriteLast.DOFade(0, StageController.actionTime);
    }

    public void OnSpriteShowUp(Sprite sprite)
    {
        spriteNow.sprite = sprite;
        spriteNow.DOFade(1, StageController.actionTime);
    }

    public void OnSpriteShowOff()
    {
        spriteNow.DOFade(0, StageController.actionTime);
    }
    
    public void SetLayer(int value)
    {
        spriteA.sortingOrder = value;
        spriteB.sortingOrder = value;
    }

    void SideChange()
    {
        switch (isSideA)
        {
            case true:
                spriteNow = spriteB;
                spriteLast = spriteA;
                break;

            case false:
                spriteNow = spriteA;
                spriteLast = spriteB;
                break;
        }

        isSideA = !isSideA;
    }
}
