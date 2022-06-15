using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using DG.Tweening;

public class PopUpUI : MonoBehaviour
{
    public UnityEvent unityEvent;
    public GameObject blocker;

    [SerializeField] DOTweenAnimation popupTween;
    bool isOn;
    
    public void ShowPopup()
    {
        isOn = true;
        BlockerSiwtch();
        popupTween.DORestart();
    }

    public void OnBtnCkicked(bool isConfirm)
    {
        if (isConfirm) unityEvent.Invoke();
        ClosePopup();
    }

    public void ClosePopup()
    {
        isOn = false;
        popupTween.DOPlayBackwards();
    }

    public void BlockerSiwtch()
    {
        blocker.SetActive(isOn);
    }
}