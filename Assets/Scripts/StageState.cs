using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class StageState
{
    public enum SpriteType {character, BG }
    public string script;
    public int count;
    public List<CharacterState> characterStates;
    public string BGM;
    public string BG;
    public string saveDate;

    static public StageState CreateFromJson(string jsonString)
    {
        return JsonUtility.FromJson<StageState>(jsonString);
    }
}