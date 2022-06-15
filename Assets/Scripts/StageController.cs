using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController : MonoBehaviour
{

    static public System.Action PrepareDoneEvent;
    static public System.Action<Sprite> ChangeBGEvent;
    static public System.Action<AudioClip> ChangeBGMEvent;
    static public System.Action<CGData, bool> CGShowEvent;
    static public System.Action<Sprite, bool> FilterActionEvent;
    static public System.Action<Sprite, bool> ItemAciotnEvent;

    [SerializeField]float _actionTime;
    static public float actionTime;
    
    [SerializeField] AudioSource audioSFX;

    void Awake()
    {
        actionTime = _actionTime;
        //ActManager.StageActEvent += OnAct;

        ActManager.ChangeScriptEvent += SetStage;
        StageActionTigger.ChangeBGMEvent += ChangeBGM;
        StageActionTigger.ChangeBGEvent += ChangeBG;
        StageActionTigger.PlaySFXEvent += PlaySFX;
        StageActionTigger.CGEvent += CGShow;
        StageActionTigger.FilterEvent += FilterAction;
        StageActionTigger.ItemEvent += ItemAction;
    }

    void Start()
    {
        SetStage();
    }

    void OnDestroy()
    {
        //ActManager.StageActEvent -= OnAct;
        ActManager.ChangeScriptEvent -= SetStage;
        StageActionTigger.ChangeBGMEvent -= ChangeBGM;
        StageActionTigger.ChangeBGEvent -= ChangeBG;
        StageActionTigger.PlaySFXEvent -= PlaySFX;
        StageActionTigger.ItemEvent -= ItemAction;
    }

    public void SetStage()
    {
        if (!ActManager.isLoadStageState)
        {
            //如果不是LOAD存檔
            Debug.Log("Not loading save");
            ChangeBG(ActManager.scriptData.startBG);
            if (ActManager.scriptData.startBGM != null) ChangeBGM(ActManager.scriptData.startBGM);
            PrepareDoneEvent();
        }else if (ActManager.isLoadStageState)
        {
            //如果是LOAD存檔
            Debug.Log("Loading save");
            StageState state = ActManager.stageState;
            ChangeBG(ResourceAddressConfig.GetBG(state.BG));
            ChangeBGM(ResourceAddressConfig.GetBGM(state.BGM));

            PrepareDoneEvent();
        }
    }

    public void ChangeBG(Sprite sprite)
    {
        ChangeBGEvent(sprite);
    }

    public void ChangeBGM(AudioClip audioClip)
    {
        ChangeBGMEvent(audioClip);
    }

    public void PlaySFX(AudioClip audioClip)
    {
        audioSFX.Stop();
        audioSFX.PlayOneShot(audioClip);
    }

    public void CGShow(CGData data, bool isShow)
    {
        CGShowEvent(data, isShow);
    }

    public void FilterAction(Sprite filter, bool isShow)
    {
        FilterActionEvent(filter, isShow);
    }

    public void ItemAction(Sprite sprite, bool isShow)
    {
        ItemAciotnEvent(sprite, isShow);
    }
}