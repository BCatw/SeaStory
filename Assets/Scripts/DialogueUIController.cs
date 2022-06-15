using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// 這裡不接PlayerBehavior，只讓GameplayHelper CALL
/// </summary>

public class DialogueUIController : MonoBehaviour
{
    [Header("Auto Set")]
    public GameObject autoTag;
    public GameObject autoStopBtn;
    public GameObject autoText;
    public DOTweenAnimation autoTextAnima;

    [Header("Dialogue UI Set")]
    public bool isHiding;
    public GameObject DialogueUIObj;
    public GameObject EndHideBtn;
    
    
    void Awake()
    {
        ActManager.SetAutoEvent += ShowAutoPlayUI;
        autoTextAnima = autoText.GetComponent<DOTweenAnimation>();
    }

    void OnDestroy()
    {
        ActManager.SetAutoEvent -= ShowAutoPlayUI;
    }

    public void ShowAutoPlayUI(bool isShow)
    {
        autoTag.SetActive(isShow);
        autoStopBtn.SetActive(isShow);
        autoText.SetActive(isShow);
        if (isShow) autoTextAnima.DORestart();
    }
    

    #region HideUI
    public void OnHideUI(bool isHide)
    {
        Debug.Log("Now is going to hide UI? [" + isHiding.ToString());
        DialogueUIObj.SetActive(!isHide);
        EndHideBtn.SetActive(isHide);
        isHiding = isHide;
        GameplayHelper.NowUIState = isHide ? GameplayHelper.UIState.Hide : GameplayHelper.UIState.None;
    }
    
    public void OnHideUI()
    {
        Debug.Log("Now is going to hide UI? [" + isHiding.ToString());
        OnHideUI(!isHiding);
    }
    #endregion
}