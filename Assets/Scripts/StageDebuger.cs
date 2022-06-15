using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageDebuger : MonoBehaviour
{
    [SerializeField] Text scriptText;
    [SerializeField] Text countText;
    
    void Start()
    {
        OnScriptUpdate();
        ActManager.NextDialogueEvent += OnNextProcess;
        ActManager.ChangeScriptEvent += OnScriptUpdate;
    }

    private void OnDestroy()
    {
        ActManager.NextDialogueEvent -= OnNextProcess;
        ActManager.ChangeScriptEvent -= OnScriptUpdate;
    }

    public void OnScriptUpdate()
    {
        var tempt = ActManager.scriptData.name.Split('_');
        scriptText.text = tempt[1] +"_"+ tempt[2];
    }

    public void OnNextProcess(int value)
    {
        countText.text = ""+value;
    }

    public void ResetProcess()
    {

    }

}
