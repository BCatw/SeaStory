using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 接收玩家操作，並發出對應EVENT、對UI進行操作
/// </summary>
public class GameplayHelper : MonoBehaviour
{
    DialogueUIController uIController;

    public enum UIState { None, Log, Menu, Hide };
    public UIState uIState;
    static public UIState NowUIState;

    static public System.Action ContinueEvent;
    static public System.Action<bool> SetAutoEvent;
    static public System.Action<bool> SkipEvent;
    static public System.Action ShowLogUIOneKeyEvent;
    static public System.Action<bool> ShowLogUIEvent;
    static public System.Action ShowMenuEvent;

    private void Awake()
    {
        uIController = GetComponent<DialogueUIController>();

        #region EventRigister
        PlyaerBehaviorManager.ContinueBehavior += OnContinue;
        PlyaerBehaviorManager.AutoBehavior += OnAutoStart;
        PlyaerBehaviorManager.SkipBehavior += OnSkip;
        PlyaerBehaviorManager.LogUIBehavior += OnCallLogUI;
        PlyaerBehaviorManager.LogUIOneKeyBehavior += OnCallLogUI;
        PlyaerBehaviorManager.MenuUIBehavior += OnCallMenu;
        PlyaerBehaviorManager.HideUIBehavior += OnHideUI;
        #endregion
    }

    private void OnDestroy()
    {
        #region EventDeregister
        PlyaerBehaviorManager.ContinueBehavior -= OnContinue;
        PlyaerBehaviorManager.AutoBehavior -= OnAutoStart;
        PlyaerBehaviorManager.SkipBehavior -= OnSkip;
        PlyaerBehaviorManager.LogUIBehavior -= OnCallLogUI;
        PlyaerBehaviorManager.LogUIOneKeyBehavior -= OnCallLogUI;
        PlyaerBehaviorManager.MenuUIBehavior -= OnCallMenu;
        PlyaerBehaviorManager.HideUIBehavior -= OnHideUI;
        #endregion
    }
    
    public void OnContinue()
    {
        if (NowUIState != UIState.None) return;
        ContinueEvent();
        SetAutoEvent(false);
    }

    public void OnAutoStart()
    {
        if (NowUIState != UIState.None) return;
        SetAutoEvent(!ActManager.IsPlayerAuto);
    }
    
    public void OnSkip()
    {
        if (NowUIState != UIState.None) return;
        OnSkip(!ActManager.IsPlayerAuto);
    }

    public void OnSkip(bool value)
    {
        if (NowUIState != UIState.None) return;
        SkipEvent(value);
    }
    
    public void OnCallLogUI()
    {
        SetAutoEvent(false);
        if (NowUIState == UIState.None || NowUIState == UIState.Log) ShowLogUIOneKeyEvent();
    }

    public void OnCallLogUI(bool isOn)
    {
        SetAutoEvent(false);
        if (NowUIState == UIState.None || NowUIState == UIState.Log) ShowLogUIEvent(isOn);
    }

    public void OnCallMenu()
    {
        SetAutoEvent(false);
        if (NowUIState == UIState.None || NowUIState == UIState.Menu) ShowMenuEvent();
    }

    public void OnHideUI()
    {
        SetAutoEvent(false);
        if (NowUIState == UIState.None || NowUIState == UIState.Hide) uIController.OnHideUI();
    }
}