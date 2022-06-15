using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMUnit : MonoBehaviour
{
    public BGMHelper bGMHelper;

    void Awake()
    {
        StageController.ChangeBGMEvent += OnChangeBGM;
    }

    void OnDestroy()
    {
        StageController.ChangeBGMEvent -= OnChangeBGM;
    }

    void OnChangeBGM(AudioClip clip)
    {
        bGMHelper.OnChangeaudio(clip);
    }

}
