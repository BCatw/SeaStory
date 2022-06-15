using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DebugerPannelController : MonoBehaviour
{
    static public System.Action<NextData> DebugNextEvent;

    [SerializeField] bool isOn = false;
    [SerializeField] float CD;
    [SerializeField] DOTweenAnimation DOAni;
    SceneArgs sceneArgs;

    private void Awake()
    {
        sceneArgs = new SceneArgs();
        sceneArgs.stateNumber = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (CD > 0) CD -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.KeypadPeriod) && CD<=0)
        {
            Debug.Log("GetKey");
            PannelSwitch();
        }
    }

    void PannelSwitch()
    {
        if (!isOn) DOAni.DORestart();
        else if (isOn) DOAni.DOPlayBackwards();
        isOn = !isOn;
        CD = 0.3f;
    }

    public void LoadScript(string value)
    {
        NextData nextData = new NextData();
        nextData.nextValue = value;
        nextData.nextDataType = NextData.NextDataType.script;

        DebugNextEvent(nextData);
    }
}