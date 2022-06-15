using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CharacterUnit : MonoBehaviour
{
    //[SerializeField] CharacterData characterData;
    public bool isOn = false;
    public SpriteHelper spriteHelper;
    public bool isFacingRight;
    public string characterName;

    private void Update()
    {
        if (spriteHelper.spriteNow.flipX != isFacingRight) Flip();
    }

    private void OnDestroy()
    {
        
    }

    public void OnCharacterSpriteChange(Sprite sprite)
    {
        spriteHelper.OnChangeSprite(sprite);
    }

    public void OnCharacterOn(Sprite sprite)
    {
        isOn = true;
        spriteHelper.OnSpriteShowUp(sprite);
    }

    public void OnCharacterOff()
    {
        isOn = false;
        spriteHelper.OnSpriteShowOff();
    }

    public void OnCharacterMove(float posit, float time)
    {
        transform.DOLocalMoveX(posit, time);
    }

    public Sprite GetSpriteNow()
    {
        Sprite sprite = spriteHelper.spriteNow.sprite;
        return sprite;
    }

    public float GetXPosit()
    {
        float value = transform.position.x;
        return value;
    }

    void Flip()
    {
        spriteHelper.spriteNow.flipX = !spriteHelper.spriteNow.flipX;
    }

    public void Animate(CharacterAnimation.AnimationType animationType)
    {
        switch (animationType)
        {
            case CharacterAnimation.AnimationType.shake:

                transform.DOShakePosition(StageController.actionTime / 2, 1, 10, 90, false, false);
                break;
        }
    }
}
