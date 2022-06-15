using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ItemHelper : MonoBehaviour
{
    public CanvasGroup canvasGroup;
    public Image fenceImage;
    public Image itemImage;

    private void Awake()
    {
        StageController.ItemAciotnEvent += OnShowItem;
    }

    private void OnDestroy()
    {
        StageController.ItemAciotnEvent -= OnShowItem;
    }

    public void OnShowItem(Sprite sprite, bool isShow)
    {
        if(sprite != null)itemImage.sprite = sprite;

        float targetAlpha = isShow ? 1 : 0;

        canvasGroup.DOFade(targetAlpha, StageController.actionTime);
    }
}
