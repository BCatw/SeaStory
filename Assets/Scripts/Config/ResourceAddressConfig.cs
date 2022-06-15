using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceAddressConfig 
{
    static public string CharacterSpritePath = "Image/Stage/Character/{0}";
    static public string BGSpritePath = "Image/Stage/BG/{0}";
    static public string BGMAudioPath = "Audios/BGM/{0}";
    static public string ScriptPath = "ScriptableObj/Script/Script_{0}";
    static public string BranchPath = "ScriptableObj/Script/BranchData_{0}";
    static public string CGDataPath = "ScriptableObj/CGData/CGData_{0}";
    static public string SFXAudioPath = "Audios/SFX/Stage/{0}";
    static public string FilterSpritePath = "Image/Stage/Filter/{0}";
    static public string ItemSpritePath = "Image/Stage/Item/{0}";

    static public ScriptData GetScriptData(string excelName)
    {
        string path = string.Format(ScriptPath, excelName);
        ScriptData data = Resources.Load<ScriptData>(path);
        return data;
    }

    static public BranchData GetBranchData(string excelName)
    {
        string path = string.Format(BranchPath, excelName);
        BranchData data = Resources.Load<BranchData>(path);
        return data;
    }

    static public AudioClip GetBGM(string fileName)
    {
        string path = string.Format(BGMAudioPath, fileName);
        AudioClip clip = Resources.Load<AudioClip>(path);
        return clip;
    }

    static public Sprite GetBG(string fileName)
    {
        string path = string.Format(BGSpritePath, fileName);
        Sprite sprite = Resources.Load<Sprite>(path);
        return sprite;
    }

    static public Sprite GetCharacterSprite(string fileName)
    {
        string path = string.Format(CharacterSpritePath, fileName);
        Sprite character = Resources.Load<Sprite>(path);
        return character;
    }

    static public AudioClip GetSFX(string fileName)
    {
        string path = string.Format(SFXAudioPath, fileName);
        AudioClip clip = Resources.Load<AudioClip>(path);
        return clip;
    }

    static public CGData GetCGData(string cgName)
    {
        string path = string.Format(SFXAudioPath, cgName);
        var data = Resources.Load<CGData>(path);
        return data;
    }

    static public GameObject GetVFX(string fileName)
    {
        string path = string.Format(SFXAudioPath, fileName);
        var obj = Resources.Load<GameObject>(path);
        return obj;
    }

    static public Sprite GetFilterSprite(string fileName)
    {
        string path = string.Format(FilterSpritePath, fileName);
        Sprite filter = Resources.Load<Sprite>(path);
        return filter;
    }

    static public Sprite GetItemSprite(string fileName)
    {
        string path = string.Format(ItemSpritePath, fileName);
        Sprite filter = Resources.Load<Sprite>(path);
        return filter;
    }
}
