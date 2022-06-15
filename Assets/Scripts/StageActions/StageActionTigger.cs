using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AciotnTrigger", menuName = "ScriptableObj/Trigger", order = 1)]
public class StageActionTigger : ScriptableObject
{
    static public System.Action<Sprite> ChangeBGEvent;
    static public System.Action<AudioClip> ChangeBGMEvent;
    static public System.Action<AudioClip> PlaySFXEvent;
    static public System.Action<CGData,bool> CGEvent;
    static public System.Action<Sprite, bool> FilterEvent;
    static public System.Action<Sprite, bool> ItemEvent;

    public void OnChangeBGM(AudioClip clip)
    {
        ChangeBGMEvent(clip);
    }

    public void OnChangeBG(Sprite sprite)
    {
        ChangeBGEvent(sprite);
    }

    public void PlaySFX(AudioClip audioClip)
    {
        PlaySFXEvent(audioClip);
    }

    public void OnCG(CGData data, bool isOn)
    {
        CGEvent(data,isOn);
    }

    public void PlayFX(GameObject gameObject)
    {
        Instantiate(gameObject);
    }

    public void OnFilter(Sprite filter, bool isOn)
    {
        FilterEvent(filter, isOn);
    }

    public void OnItem(Sprite sprite, bool isOn)
    {
        ItemEvent(sprite, isOn);
    }
}
