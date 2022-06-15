using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ActEventConfig
{
    public enum CharacterPosition { ExLeft, Left, Center, Right, ExRight}
}

[System.Serializable]
public class StageAct
{
    public enum ActCategory
    {
        changeBGM,
        changeBG,
        playFX
    }

    public ActCategory actCategory;
    public Sprite BGSprite;
    public AudioClip BGMAudioClip;
    public AudioClip SFXAudioClip;
    public GameObject FXObj;
}

[System.Serializable]
public class CharacterAct
{
    public enum SpriteAct { On, Off, Chamge, none };

    public string characterName;
    public SpriteAct spriteAct;
    public Sprite sprite;
    public bool isMoving = false;
    public bool immediatelyMoving = false;
    public float xPosition;
    public bool isFacingRight = false;
    public int layer = 0;
    public CharacterAnimation.AnimationType animationType;
}