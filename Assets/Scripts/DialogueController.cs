using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using DG.Tweening;
using TMPro;

/// <summary>
/// 負責文字輸出
/// </summary>

public class DialogueController : MonoBehaviour
{

    static public event System.EventHandler<DialogueSingleData> DialogueDoneHandler;
    static public System.Action PrepareDoneEvent;
    static public System.Action PlayDoneEvent;

    //資料
    [SerializeField] public List<DialogueSingleData> dialogueDataPool;
    [SerializeField] public int countNow;
    [SerializeField] int countLast;
    [SerializeField] int countDialogue;
    [SerializeField] bool isDone;
    [SerializeField] bool isSkip = false;
    [SerializeField] bool isGameOver = false;
    [SerializeField] float doneWaitTime;
    [SerializeField] DOTweenAnimation nextHintTween;
    [SerializeField] bool inTag;
    [SerializeField] List<int> tagStartPosit;
    [SerializeField] List<int> tagEndPosit;
    [SerializeField] List<int> noneTagPosit;

    string originDialogue;
    DialogueSingleData nowDD;
    DialogueSingleData lastDD;

    [SerializeField] public int countStart;
    [SerializeField] bool isAutoFast;
    [SerializeField] bool isDialogueNull;
    Coroutine dialogueWriter;

    //演出
    [SerializeField] float defaultSpeakSpeed;
    [SerializeField] float speakSpeed;
    
    //UI
    [SerializeField] Text nameText;
    //[SerializeField] Text dialogueText;
    [SerializeField] TextMeshProUGUI dialogueText;
    [SerializeField] GameObject black_UI;
    [SerializeField] Button continueBtn;

    public UnityEvent OnGameOver;
    public UnityEvent OnDialogueDone;

    void Awake()
    {
        ActManager.NextDialogueEvent += LoadDialogueData;
        ActManager.ResetStageEvent += DeleteDialogue;
        ActManager.ChangeScriptEvent += LoadDialogueDataPool;
        ActManager.SkipProcessEvent += OnSkip;
        ActManager.SkipScripyEvent += OnScriptSkip;
        ActManager.AutoFastEvent += OnAutoFast;
        OptionUI.OptionShowEvent += OnOptionShow;
        OptionUI.BranchDialogueTextEvent += LoadDialogueString;
        LoadDialogueDataPool();
    }

    void OnDestroy()
    {
        ActManager.NextDialogueEvent -= LoadDialogueData;
        ActManager.ResetStageEvent -= DeleteDialogue;
        ActManager.ChangeScriptEvent -= LoadDialogueDataPool;
        ActManager.SkipProcessEvent -= OnSkip;
        ActManager.SkipScripyEvent -= OnScriptSkip;
        ActManager.AutoFastEvent -= OnAutoFast;
        OptionUI.OptionShowEvent -= OnOptionShow;
    }

    void LoadDialogueDataPool()
    {
        dialogueDataPool = ActManager.scriptData.dialogueSingleDatas;
        dialogueText.text = "";
        countDialogue = dialogueDataPool.Count - 1;
        PrepareDoneEvent();
    }

    public void OnSkip()
    {
        isSkip = true;
        continueBtn.interactable = false;
    }
    
    public void OnAutoFast(bool value)
    {
        isAutoFast = value;
    }

    //載入對話資料
    void LoadDialogueData(int value)
    {
        lastDD = nowDD;
        nowDD = dialogueDataPool[value];
        if (lastDD.ID == nowDD.ID) return;
        speakSpeed = defaultSpeakSpeed;
        isDialogueNull = nowDD.dialogue == "null" ? true : false;
        black_UI.SetActive(!isDialogueNull);
        
        //更換名稱
        if (nowDD.dialogue == "" || nowDD.characterName == "OS")
        {
            nameText.text = "";
            dialogueText.fontStyle = FontStyles.Italic;
        }
        else
        {
            nameText.text = nowDD.characterName;
            dialogueText.fontStyle = FontStyles.Normal;
        }

        if (nowDD.isRefresh)
        {
            originDialogue = "";
            DeleteDialogue();
        }
        else if (!nowDD.isRefresh) originDialogue = dialogueText.text;

        dialogueWriter = StartCoroutine(DialogueWritter(nowDD.dialogue));
    }

    void LoadDialogueString(string dialogue)
    {
        StopCoroutine(dialogueWriter);
        originDialogue = "";
        DeleteDialogue();
        dialogueWriter = StartCoroutine(DialogueWritter(dialogue));
    }

    //更新顯示文字
    IEnumerator DialogueWritter(string dialogue)
    {
        Debug.Log("[" + System.DateTime.Now + "] StartWriteDialogue");
        isDone = false;
        NextHintSwitch(isDone);

        //文字存入暫存
        char[] chars = dialogue.ToCharArray();

        //剖析字串
        TagCheck(chars);
        
        bool isWithTag = tagStartPosit.Count>=1 && tagEndPosit.Count >= 1 && tagStartPosit[0] < tagEndPosit[0] ? true : false;
        
        string temptString = "";

        if (!isDialogueNull)
        {
            switch (isWithTag)
            {
                case true:
                    //先將tag輸入
                    Debug.Log("CC");
                    for (int i = 0; i < chars.Length; i++)
                    {
                        Debug.Log("CC-1");
                        for (int x = 0; x < tagStartPosit.Count; x++)
                        {
                            Debug.Log("CC-2_ x: " + x + " i: " + i);
                            if (i >= tagStartPosit[x] && i <= tagEndPosit[x])
                            {
                                Debug.Log("CC-3");
                                Debug.Log("CC-4: " + chars[i]);
                                temptString += chars[i];
                            }
                        }
                    }
                    Debug.Log("CC-4");
                    dialogueText.text += temptString;


                    //逐一輸入剩餘字串
                    for (int i = 0; i < chars.Length; i++)
                    {
                        bool isIn = true;

                        for (int x = 0; x < tagStartPosit.Count; x++)
                        {
                            if (i >= tagStartPosit[x] && i <= tagEndPosit[x])
                            {
                                isIn = true;
                                break;
                            }
                            else isIn = false;
                        }

                        if (!isIn)
                        {
                            if (!isSkip && !isAutoFast)
                            {

                                yield return new WaitForSeconds(speakSpeed);
                                temptString = temptString.Insert(i, chars[i].ToString());
                                dialogueText.text = originDialogue + temptString;
                            }
                            else if (isSkip | isAutoFast)
                            {
                                StartCoroutine(DialogueImmidiatlyDone());
                                break;
                            }
                        }
                    }
                    break;

                case false:

                    for (int i = 0; i < chars.Length; i++)
                    {
                        if (!isSkip && !isAutoFast)
                        {
                            yield return new WaitForSeconds(speakSpeed);
                            temptString = temptString.Insert(i, chars[i].ToString());
                            dialogueText.text = originDialogue + temptString;
                        }
                        else if (isSkip | isAutoFast)
                        {
                            StartCoroutine(DialogueImmidiatlyDone());
                            break;
                        }
                    }
                    break;
            }
        }

        //重置
        yield return new WaitForSeconds(doneWaitTime);
        PlayDoneEvent();
        Debug.Log("[" + System.DateTime.Now + "] PlayDoneEvent");
        DialogueDone();
        DialogueDoneHandler(this, nowDD);
    }
    
    //對話結束操作冷卻
    IEnumerator DialogueImmidiatlyDone()
    {
        isSkip = true;
        dialogueText.text = originDialogue + nowDD.dialogue;
        yield return new WaitForSeconds(doneWaitTime);
        DialogueDone();
    }

    void DialogueDone()
    {
        OnDialogueDone.Invoke();
        continueBtn.interactable = true;
        isDone = true;
        isSkip = false;
        NextHintSwitch(isDone);
    }
    
    void NextHintSwitch(bool isOn)
    {
        nextHintTween.gameObject.SetActive(isOn);
        if (isOn) nextHintTween.DORestart(); 
    }

    //刪除對話
    void DeleteDialogue()
    {
        dialogueText.text = "";
    }

    //確認tag
    void TagCheck(char[] fulltext)
    {
        tagStartPosit = new List<int>();
        tagEndPosit = new List<int>();
        noneTagPosit = new List<int>();

        for (int i = 0; i< fulltext.Length; i++)
        {
            if (fulltext[i] == '<') tagStartPosit.Add(i);
            if (fulltext[i] == '>') tagEndPosit.Add(i);
            else if (fulltext[i] != '>' && fulltext[i] != '<') noneTagPosit.Add(i);
        }
    }

    //顯示、隱藏UI
    void ShowUI(bool value)
    {

    }

    //選項出現時，開關繼續按鈕
    void OnOptionShow(bool value)
    {
        continueBtn.interactable = !value;
    }

    void OnScriptSkip()
    {
        DeleteDialogue();
        LoadDialogueData(countDialogue-1);
    }

}