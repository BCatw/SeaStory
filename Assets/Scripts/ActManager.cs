using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 導演，控制場景進度
/// </summary>



public class ActManager : MonoBehaviour
{
    #region ref data
    static public bool isLoadStageState { get; set; }
    static public ScriptData scriptData { get; set; }
    static public StageState stageState { get; set; }
    public int processMax;
    [SerializeField] int processStart;
    [SerializeField] bool m_isLoadingSave;
    #endregion

    [SerializeField] bool isGameOver;
    [SerializeField] bool isStageDone;
    [SerializeField] bool isDialogueDone;
    [SerializeField] bool isPrepare;
    int nextStageAct = 0;
    [SerializeField]bool isProcessDone;
    [SerializeField] float fastTimeScale = 20.0f;
    [SerializeField] float defaultTimeScle = 1.0f;

    #region events
    static public System.Action<int> NextDialogueEvent;
    static public System.Action ResetStageEvent;
    static public System.Action PrepareDoneEvent;
    static public System.Action PrepareDone;
    static public System.EventHandler<SceneArgs> SceneStateChanged;
    static public System.Action ChangeScriptEvent;
    static public System.Action SkipProcessEvent;
    static public System.Action SkipScripyEvent;
    static public System.Action<StageAct> StageActEvent;
    static public System.Action<CharacterAct[]> CharacterActEvent;
    static public System.Action CallPopUpEvent;
    static public System.Action<BranchData> BranchStartEvent;
    static public System.Action<string> EndingEvent;
    static public System.Action<bool> AutoFastEvent;
    static public System.Action<bool> SetAutoEvent;
    #endregion


    public int processNow;
    public bool isBranch;
    [SerializeField] bool isPlayerAuto;
    [SerializeField] bool isActAuto;
    [SerializeField] float autoWaitTime;
    static public bool IsPlayerAuto = false;
    
    private void Awake()
    {
        StageController.PrepareDoneEvent += OnStagePrepareDone;
        DialogueController.PrepareDoneEvent += OnDialoguePrepareDone;
        DialogueController.PlayDoneEvent += OnDialoguePlayDone;
        Covin19Demo.BackTitleEvent += OnBackToTitle;
        OptionUI.OptionChoseEvent += LoadNext;
        DebugerPannelController.DebugNextEvent += LoadNext;
        OptionUI.OptionShowEvent += SetAutoPlay;

        #region GameplayEvent
        GameplayHelper.ContinueEvent += PlayNextProcess;
        GameplayHelper.SetAutoEvent += SetAutoPlay;
        GameplayHelper.SkipEvent += SetAutoFast;
        #endregion
    }

    void Update()
    {
        m_isLoadingSave = isLoadStageState;

        if(isStageDone && isDialogueDone && !isPrepare)
        {
            Debug.Log("AllPrepareDone");
            PrepareDone();
            PlayProcess(processStart);
            isPrepare = true;
            isGameOver = false;
        }
    }
    
    void OnStagePrepareDone()
    {
        isStageDone = true;
    }

    void OnDialoguePrepareDone()
    {
        isDialogueDone = true;
    }
    
    public void LoadScript()
    {
        isGameOver = true;
        isStageDone = false;
        isDialogueDone = false;
        isPrepare = false;
        isBranch = false;

        processNow = 0;

        scriptData = GetData(PlayerPrefs.GetString("LoadingScript"), "Script") as ScriptData;
        processStart = PlayerPrefs.GetInt("ProcessStart");
        processMax = scriptData.dialogueSingleDatas.Count;

        Debug.Log("Script name is " + scriptData.name);
        Debug.Log("BG name is " + scriptData.startBG.name);
        //Debug.Log("First dialogue is <color = blue>" + scriptData.dialogueData.dialogueDatas[processStart].dialogue +"</color>");
    }
    
    public ScriptData GetRecentScript()
    {
        return scriptData;
    }

    public void ResetStage()
    {
        ResetStageEvent.Invoke();
        processNow = 0;
        nextStageAct = 0;
    }

    public void PlayNextProcess()
    {
        if (isGameOver) return;
        if (isBranch)
        {
            SetAutoPlay(false);
            return;
        }

        Debug.Log("[" + System.DateTime.Now + "] PNP");
        if (isProcessDone)
        {
            processNow++;
            if (processNow < processMax)
            {
                isProcessDone = false;
                PlayProcess(processNow);
            }
            else if (processNow >= processMax)
            {
                Debug.Log("processNow >= processMax");
                LoadNext();
            }
        }
        else if (!isProcessDone)
        {
            SkipProcess();
        }
    }

    public void OnDialoguePlayDone()
    {
        isProcessDone = true;
        if (isActAuto|| isPlayerAuto)
        {
            StartCoroutine(AutoWait());
        }
    }

    IEnumerator AutoWait()
    {
        yield return new WaitForSeconds(autoWaitTime);
        PlayNextProcess();
    }
    
    public void PlayProcess(int value)
    {
        isActAuto = scriptData.dialogueSingleDatas[value].isAutoNex;
        Debug.Log("value is " + value);
        processNow = value;
        NextDialogueEvent(processNow);
        scriptData.dialogueSingleDatas[processNow].OnAct();
        
        if (scriptData.characterProcesses[value].isAct)
        {
            CharacterActEvent(scriptData.characterProcesses[value].characterActs);
        }
    }

    public void SkipProcess()
    {
        SkipProcessEvent();
    }

    public void SkipScript()
    {
        SkipScripyEvent();
        LoadNext();
    }

    #region Auto
    public void SetAutoPlay(bool isStart)
    {
        if (!isStart)
        {
            Time.timeScale = defaultTimeScle;
            AutoFastEvent(isStart);
        }

        isPlayerAuto = isStart;
        IsPlayerAuto = isPlayerAuto;
        SetAutoEvent(isPlayerAuto);
        if (isProcessDone && isStart)
        {
            PlayNextProcess();
        }
    }

    public void SetAutoFast(bool isStart)
    {
        Time.timeScale = fastTimeScale;
        AutoFastEvent(isStart);
        SetAutoPlay(isStart);
    }
    /*
    public void StartAutoPlay()
    {
        isPlayerAuto = true;
        SetAutoEvent(isPlayerAuto);
        if (isProcessDone)
        {
            PlayNextProcess();
        }
    }
        
    public void EndAutoPlay()
    {
        Time.timeScale = defaultTimeScle;
        AutoFastEvent(false);
        isPlayerAuto = false;
        SetAutoEvent(isPlayerAuto);
    }

    public void EndAutoPlay(bool b)
    {
        Time.timeScale = defaultTimeScle;
        AutoFastEvent(false);
        isPlayerAuto = false;
        SetAutoEvent(isPlayerAuto);
    }
    */
    #endregion

    public void LoadNext()
    {
        int v = 0;
        if (scriptData.isNextRefPref) v = PlayerPrefs.GetInt(scriptData.nextRefPrefName);
        NextData nextData = scriptData.nextData[v];

        isLoadStageState = false;
        isBranch = nextData.nextDataType == NextData.NextDataType.option;

        switch (nextData.nextDataType)
        {
            case NextData.NextDataType.script:
                Debug.Log("Next is Script, loading " + nextData.nextValue);
                PlayerPrefs.SetString("LoadingScript", nextData.nextValue);
                PlayerPrefs.SetInt("ProcessStart",0);
                LoadScript();
                ChangeScriptEvent();
                break;

            case NextData.NextDataType.option:
                Debug.Log("Next is Option, loading " + nextData.nextValue);
                var data = ResourceAddressConfig.GetBranchData(nextData.nextValue);
                SetAutoPlay(false);
                BranchStartEvent(data);
                break;

            case NextData.NextDataType.end:
                isGameOver = true;
                SetAutoPlay(false);
                EndingEvent(nextData.nextValue);
                break;
            }
        }

    public void LoadNext(NextData nextData)
    {

        isLoadStageState = false;
        isBranch = nextData.nextDataType == NextData.NextDataType.option;

        switch (nextData.nextDataType)
        {
            case NextData.NextDataType.script:
                Debug.Log("Next is Script, loading " + nextData.nextValue);
                PlayerPrefs.SetString("LoadingScript", nextData.nextValue);
                PlayerPrefs.SetInt("ProcessStart", 0);
                LoadScript();
                ChangeScriptEvent();
                break;

            case NextData.NextDataType.option:
                SetAutoPlay(false);
                Debug.Log("Next is Option, loading " + nextData.nextValue);
                var data = ResourceAddressConfig.GetBranchData(nextData.nextValue);
                Time.timeScale = 1.0f;
                BranchStartEvent(data);
                break;

            case NextData.NextDataType.end:
                isGameOver = true;
                SetAutoPlay(false);
                Time.timeScale = 1.0f;
                EndingEvent(nextData.nextValue);
                break;
        }
    }
    
    Object GetData(string value, string type)
    {
        var data = ResourceAddressConfig.GetScriptData(value);
        Debug.Log("Act Find Data Name is: " + data.name);
        return data;
    }

    void OnBackToTitle()
    {
        SceneArgs sceneArgs = new SceneArgs();
        sceneArgs.stateNumber = 0;
        SceneStateChanged(this, sceneArgs);
    }

    public void LoadStageState(StageState state)
    {
        Debug.Log("Act start LSS");
        isPrepare = false;
        stageState = state;
        LoadScript();
    }
}