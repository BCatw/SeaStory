using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CGUI_Monitor : MonoBehaviour
{
    [SerializeField] Image cGImage;
    [SerializeField] SpriteRenderer cGSprite;
    [SerializeField] float fadeTime;
    [SerializeField] Image blocker;
    [SerializeField] Button closeBtn;
    [SerializeField] bool isShowing = false;
    [SerializeField] DOTweenAnimation cGTweening;
    [SerializeField] GameObject obj;

    void Awake()
    {
        CGUI_Unit.ShowCGEvent += OnShowCG;
        CGUI_Unit.ShowNothingEvent += OnShowNothing;

        blocker.raycastTarget = false;
    }

    private void OnDestroy()
    {
        CGUI_Unit.ShowCGEvent -= OnShowCG;
        CGUI_Unit.ShowNothingEvent -= OnShowNothing;
    }

    void OnShowCG(Sprite sprite)
    {   
        cGImage.sprite = sprite;
        cGImage.DOFade(1, fadeTime).onComplete += OnFadeComplete;
        blocker.DOFade(1, fadeTime);
        //closeBtn.targetGraphic.DOFade(1, fadeTime);
        closeBtn.targetGraphic.raycastTarget = true;

    }

    public void OnDisapear()
    {
        if (isShowing == true)
        {
            cGImage.DOFade(0, fadeTime).onComplete += OnFadeComplete;
            blocker.DOFade(0, fadeTime);
            //closeBtn.targetGraphic.DOFade(0, fadeTime);
        }
    }

    void OnShowNothing()
    {
        Debug.Log("This CG is locked");
    }

    void OnFadeComplete()
    {
        isShowing = !isShowing;
        if (!isShowing) cGImage.sprite = null;
        blocker.raycastTarget = isShowing;
        closeBtn.interactable = isShowing;
        closeBtn.targetGraphic.raycastTarget = isShowing;
    }
}
