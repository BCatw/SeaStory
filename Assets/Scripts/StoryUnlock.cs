using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class StoryUnlock : MonoBehaviour
{
    [SerializeField] Image nieImage;
    [SerializeField] Image tuoImage;
    [SerializeField] Image unlockImage;

    [SerializeField]bool isUnlocked = false;

    TweenCallback tweenCallback;
    System.Action action;


    Button btn;


    void Awake()
    {
        tweenCallback += OnUnlockDone;
        btn = gameObject.GetComponent<Button>();

    }

    private void OnEnable()
    {
        Check();
    }

    public void Check()
    {
        Debug.Log("Checke Start");
        Color colorOn = new Color(1, 1, 1, 1);
        Color colorOff = new Color(1, 1, 1, 0);

        int isNie = PlayerPrefs.GetInt("isNie");
        int isTuo = PlayerPrefs.GetInt("isTuo");
        int isUnlock = PlayerPrefs.GetInt("isUnlock");

        isUnlocked = isUnlock >= 1 ? true:false;
        btn.interactable = isUnlocked;

        switch (isUnlocked)
        {
            case false:
                unlockImage.color = colorOff;
                nieImage.color = isNie >= 1 ? colorOn : colorOff;
                tuoImage.color = isTuo >= 1 ? colorOn : colorOff;

                var checker = isNie >= 1 && isTuo >= 1 ? true : false;
                if (checker) Unlock();
                break;

            case true:
                unlockImage.color = colorOn;
                break;
        }

    }

    void Unlock()
    {
        unlockImage.DOFade(1, 2).OnComplete(tweenCallback).SetDelay(1);
        PlayerPrefs.SetInt("isUnlock", 1);
    }

    void OnUnlockDone()
    {
        btn.interactable = true;
    }

}
