using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public struct DialogueSingleData
{
    public string ID;
    public string characterName;
    public bool isRefresh;
    public bool isAutoNex;
    [TextArea] public string dialogue;
    public Sprite BG;
    public AudioClip BGM;
    public AudioClip SFX;
    public GameObject VFX;
    public CGData cgData;
    public enum CGBehaviors { none, on, off};
    public CGBehaviors cgBehavior;
    public Sprite filter;
    public CGBehaviors filterBehavior;
    public Sprite iteim;
    public CGBehaviors itemBehavior;

    public void OnAct()
    {
        var trigger = new StageActionTigger();
        if (BG != null) trigger.OnChangeBG(BG);
        if (BGM != null) trigger.OnChangeBGM(BGM);
        if (SFX != null) trigger.PlaySFX(SFX);
        if (VFX != null) trigger.PlayFX(VFX);
        switch (cgBehavior)
        {
            case CGBehaviors.none:
                break;

            case CGBehaviors.on:
                trigger.OnCG(cgData, true);
                break;

            case CGBehaviors.off:
                trigger.OnCG(cgData, false);
                break;
        }
        switch (filterBehavior)
        {
            case CGBehaviors.none:
                break;

            case CGBehaviors.on:
                trigger.OnFilter(filter,true);
                break;

            case CGBehaviors.off:
                trigger.OnFilter(filter, false);
                break;
        }
        switch (itemBehavior)
        {
            case CGBehaviors.on:
                trigger.OnItem(iteim, true);
                break;
            case CGBehaviors.off:
                trigger.OnItem(iteim, false);
                break;
            default:
                break;
        }
    }
}


[CreateAssetMenu(fileName ="DialoguePool" ,menuName = "ScriptableObj/DialoguePool", order = 1)]
public class DialogueData : ScriptableObject
{
    public List<DialogueSingleData> dialogueSingleDatas;
    public string excelFileName;
    
    public void OnCreate()
    {
        List<Dictionary<string, object>> data = CSVReader.Read(excelFileName);

        string debugString = null;
        Dictionary<string, object>.KeyCollection keyColl = data[0].Keys;
        foreach (string s in keyColl)
        {
            debugString += ("Key = {0}", s);
        }

        Debug.Log("List count: " + data.Count + debugString);

        for (var i = 0; i < data.Count; i++)
        {

            DialogueSingleData newData = new DialogueSingleData();

            //寫入角色名稱
            newData.characterName = data[i]["Character Name"].ToString();
            Debug.Log("data0: " + data[i]["Character Name"].ToString());

            //寫入對話
            newData.dialogue = data[i]["Dialogue"].ToString();
            Debug.Log("data1: " + data[i]["Dialogue"].ToString());

            //寫入自動更新
            newData.isRefresh = data[i]["Refresh"].ToString() == "TRUE" ? true : false;
            Debug.Log("data2: " + data[i]["Refresh"].ToString());

            //寫入自動下一句
            newData.isAutoNex = data[i]["Auto Next"].ToString() == "TRUE" ? true : false;
            Debug.Log("data3: " + data[i]["Auto Next"].ToString());

            dialogueSingleDatas.Add(newData);
        }
    }
}