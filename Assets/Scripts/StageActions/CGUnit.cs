using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGUnit : MonoBehaviour
{
    [SerializeField] SpriteHelper spriteHelper;

    private void Awake()
    {
        StageController.CGShowEvent += CGShow;
    }

    private void OnDestroy()
    {
        StageController.CGShowEvent -= CGShow;
    }

    public void CGShow(CGData data, bool isShow)
    {
        switch (isShow)
        {
            case true:
                CGList.CGUnlock(data);
                spriteHelper.OnSpriteShowUp(data.CG);
                break;

            case false:
                spriteHelper.OnSpriteShowOff();
                break;
        }
    }
}
