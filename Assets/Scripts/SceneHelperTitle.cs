using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneHelperTitle : MonoBehaviour
{
    static public event System.EventHandler<SceneArgs> SceneStateChanged;
    static public System.Action PrepareDoneEvent;
    SceneArgs sceneArgs;

    void Awake()
    {
        PrepareDoneEvent();
        sceneArgs = new SceneArgs();
        sceneArgs.stateNumber = 1;
    }

    public void OnBtnClicked(string value)
    {
        switch (value)
        {
            case "start":
                ActManager.isLoadStageState = false;
                SceneStateChanged(this, sceneArgs);
                break;

            case "load":
                Debug.Log("Load");
                break;

            case "exit":
                Debug.Log("Exit");
                Debug.Log("Quit Game");
                Application.Quit();
                break;
        }
    }

    public void LoadScript(string value)
    {
        PlayerPrefs.SetString("LoadingScript", value);
        PlayerPrefs.SetInt("ProcessStart", 0);
        SceneStateChanged(this, sceneArgs);
    }

    public void LoadData(int value)
    {

    }
}