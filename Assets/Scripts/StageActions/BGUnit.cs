using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGUnit : MonoBehaviour
{
    public SpriteHelper spriteHelper;
    
    void Awake()
    {
        StageController.ChangeBGEvent += OnChangeBG;
        spriteHelper.SetLayer(SpriteLayerConfig.BGLayer);
    }

    void OnDestroy()
    {
        StageController.ChangeBGEvent -= OnChangeBG;
    }

    void OnChangeBG(Sprite sprite)
    {
        spriteHelper.OnChangeSprite(sprite);
    }
    
}
