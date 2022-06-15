using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilterUnit : MonoBehaviour
{
    [SerializeField] SpriteHelper spriteHelper;

    private void Awake()
    {
        StageController.FilterActionEvent += FilterAction;
        spriteHelper.SetLayer(SpriteLayerConfig.FilterLayer);
    }

    private void OnDestroy()
    {
        StageController.FilterActionEvent -= FilterAction;
    }

    public void FilterAction(Sprite filter, bool isShow)
    {
        switch (isShow)
        {
            case true:
                spriteHelper.OnSpriteShowUp(filter);
                break;

            case false:
                spriteHelper.OnSpriteShowOff();
                break;
        }
    }
}
