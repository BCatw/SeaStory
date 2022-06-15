using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 存檔時，烘焙目前廠景給存檔用
/// </summary>

public class StageBaker : MonoBehaviour
{
    [SerializeField] CharacterController characterController;
    [SerializeField] StageController stageController;
    [SerializeField] BGUnit bG;
    [SerializeField] BGMUnit bGM;

    static public System.Action<StageState> SendBakeEvent;
    
    private void Awake()
    {
        SaveAndLoad.RequireBakeEvent += OnRequiredBake;   
    }

    private void OnDestroy()
    {
        SaveAndLoad.RequireBakeEvent -= OnRequiredBake;
    }

    void OnRequiredBake()
    {
        StageState state = new StageState();

        state.characterStates = characterController.BakeCharacter();
        state.BGM = bGM.bGMHelper.audioNow.clip.name;
        state.BG = bG.spriteHelper.spriteNow.sprite.name;
        
        SendBakeEvent(state);
    }
}
