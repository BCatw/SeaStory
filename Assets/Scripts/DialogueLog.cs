using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DialogueLog : MonoBehaviour
{
    [Header("Refer")]
    [SerializeField] TagImageObj AvatarList;
    [SerializeField] Transform listParent;
    [SerializeField] GameObject logUnitPrefab;
    [SerializeField] Scrollbar scrollbar;

    [Space]
    [SerializeField] DOTweenAnimation logBlockerAnima;
    [SerializeField] DOTweenAnimation logListAnima;

    [Space]
    [SerializeField] AudioSource SFXAudioSource;
    [SerializeField] AudioClip SFXClip;

    [Header("For Debug")]
    [SerializeField] DialogueLogUnit NowlogUnit;
    [SerializeField] bool isInstatiate = false;
    [SerializeField] bool isShowing = false;
    
    DialogueSingleData nowDD;
    DialogueSingleData lastDD;

    void Awake()
    {
        GameplayHelper.ShowLogUIEvent += OnShowLog;
        GameplayHelper.ShowLogUIOneKeyEvent += OnShowLog;
        DialogueController.DialogueDoneHandler += OnDialogueDone;
    }
    
    private void OnDestroy()
    {
        GameplayHelper.ShowLogUIEvent -= OnShowLog;
        GameplayHelper.ShowLogUIOneKeyEvent -= OnShowLog; 
        DialogueController.DialogueDoneHandler -= OnDialogueDone;
    }

    void OnDialogueDone(object sender, DialogueSingleData data)
    {
        lastDD = nowDD;
        nowDD = data;
        if (nowDD.ID == lastDD.ID) return;

        Debug.Log("[" + System.DateTime.Now + "] WriteLog "+ nowDD.dialogue);
        if (nowDD.dialogue != "null")
        {

            if (nowDD.isRefresh)
            {
                NowlogUnit = GameObject.Instantiate(logUnitPrefab, listParent).GetComponent<DialogueLogUnit>();
            }

            NowlogUnit.avatar.sprite = AvatarList.GetImage(nowDD.characterName);
            NowlogUnit.nameText.text = data.characterName;
            NowlogUnit.contentText.text += data.dialogue;

            NowlogUnit.gameObject.SetActive(true);
            isInstatiate = true;
        }
    }

    public void OnShowLog(bool isShow)
    {
        if (isShow == isShowing) return;

        SFXAudioSource.PlayOneShot(SFXClip);
        if (isShow)
        {
            scrollbar.value = 0;
            logBlockerAnima.DORestart();
            logListAnima.DORestart();
            GameplayHelper.NowUIState = GameplayHelper.UIState.Log;
        }
        else
        {
            logBlockerAnima.DOPlayBackwards();
            logListAnima.DOPlayBackwards();
            GameplayHelper.NowUIState = GameplayHelper.UIState.None;
        }

        isShowing = isShow;
    }

    public void OnShowLog()
    {
        OnShowLog(!isShowing);
    }

    void Update()
    {
        if (isInstatiate)
        {
            scrollbar.value = 0;
            isInstatiate = false;
        }
    }

    void OnEnable()
    {
        scrollbar.value = 0;
    }
}