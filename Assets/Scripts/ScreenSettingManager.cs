using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenSettingManager : MonoBehaviour
{

    void Awake()
    {
        ScreenSettingHelper.ChangeFullScreenEvent += FullScreenChange;
    }

    public void SetResolution(int w, int h)
    {
        Screen.SetResolution(w, h, Screen.fullScreen);
    }

    public void FullScreenChange(bool value)
    {
        Screen.fullScreen = value;
    }

}
