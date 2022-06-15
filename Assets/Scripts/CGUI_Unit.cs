using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CGUI_Unit : MonoBehaviour
{
    public Image cGIcon;
    public CGData cGData;
    static public System.Action<Sprite> ShowCGEvent;
    static public System.Action ShowNothingEvent;

    public void WriteData(CGData data)
    {
        cGData = data;
        if(CGList.isUnlock(cGData)) cGIcon.sprite = cGData.cover;
    }

    public void OnClick()
    {
        switch(CGList.isUnlock(cGData))
        {
            case true:
                ShowCGEvent(cGData.CG);
                break;

            case false:
                ShowNothingEvent();
                break;
        }
    }

}
