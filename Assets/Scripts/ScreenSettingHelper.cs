using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenSettingHelper : MonoBehaviour
{
    [SerializeField] Toggle isFSToggle;
    static public System.Action<bool> ChangeFullScreenEvent;

    void Awake()
    {
        isFSToggle.isOn = Screen.fullScreen;
    }

    public void OnChangeFullScreen(bool value)
    {
        ChangeFullScreenEvent(value);
    }
}
