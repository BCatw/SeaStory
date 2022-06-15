using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[System.Serializable]
public struct CharacterProcess
{
    public bool isAct;
    public CharacterAct[] characterActs;
}

[System.Serializable]
public struct StartCharacter
{
    public string characterName;
    public Sprite characterSprite;
    public float startPositX;
}

[System.Serializable]
[CreateAssetMenu(fileName = "Script", menuName = "ScriptableObj/Scripts", order = 1)]
public class ScriptData : ScriptableObject
{
    [Header("[Script Info]")]
    public string excelFileName;
    public string sceneName;
    public string[] characterName;
    public Sprite cover;
    [Space]
    
    [Header("[Next Info]")]
    public bool isNextRefPref;
    public string nextRefPrefName;
    public NextData[] nextData;

    [SerializeField] int nowProcess;
    [Space]
    [Header("[Start Setting]")]
    public Sprite startBG;
    public AudioClip startBGM;
    public StartCharacter[] startCharacters;
    [Space]
    [Header("Prcoess Datas")]
    //public DialogueData dialogueData;
    public List<DialogueSingleData> dialogueSingleDatas;
    //public ActProcess[] actProcesses;
    public CharacterProcess[] characterProcesses;

    public void OnLoadData()
    {
        List<Dictionary<string, object>> data = CSVReader.Read(excelFileName);

        string debugString = null;
        Dictionary<string, object>.KeyCollection keyColl = data[0].Keys;
        foreach (string s in keyColl)
        {
            debugString += ("Key = {0}", s);
        }

        Debug.Log("List count: " + data.Count + debugString);

        if (dialogueSingleDatas.Count < data.Count)
        {
            dialogueSingleDatas.Clear();
            for (var i = 0; i < data.Count; i++)
            {
                DialogueSingleData newData = new DialogueSingleData();
                newData = WriteData(newData, data[i]);
                dialogueSingleDatas.Add(newData);
            }
        }
        else if (dialogueSingleDatas.Count >= data.Count)
        {
            for (var i = 0; i < data.Count; i++)
            {
                dialogueSingleDatas[i] = WriteData(dialogueSingleDatas[i], data[i]);
            }
        }
    }

    DialogueSingleData WriteData(DialogueSingleData data, Dictionary<string, object> value)
    {
        //寫入ID
        data.ID = value["ID"].ToString();
        Debug.Log("======" + "ID: " + value["ID"].ToString() + "======");

        //寫入角色名稱
        data.characterName = value["Character Name"].ToString();
        Debug.Log("data0: " + value["Character Name"].ToString());

        //寫入對話
        data.dialogue = value["Dialogue"].ToString();
        Debug.Log("ID: " + value["ID"].ToString() +  " data1: " + value["Dialogue"].ToString());

        //寫入自動更新
        data.isRefresh = value["Refresh"].ToString() == "TRUE" ? true : false;
        Debug.Log("ID: " + value["ID"].ToString() + " data2: " + value["Refresh"].ToString());

        //寫入自動下一句
        data.isAutoNex = value["Auto Next"].ToString() == "TRUE" ? true : false;
        Debug.Log("ID: " + value["ID"].ToString() + " data3: " + value["Auto Next"].ToString());


        //演出物件
        string tempts = value["BG"].ToString();
        object cGData = new object();

        //寫入背景
        data.BG = (Sprite)GetResource(tempts, "BG");
        Debug.Log("ID: " + value["ID"].ToString() + " data4: " + tempts);

        //寫入BGM
        tempts = value["BGM"].ToString();
        data.BGM = (AudioClip)GetResource(tempts, "BGM");
        Debug.Log("ID: " + value["ID"].ToString() + " data4: " + tempts);

        //寫入SFX
        tempts = value["SFX"].ToString();
        data.SFX = (AudioClip)GetResource(tempts, "SFX");
        Debug.Log("ID: " + value["ID"].ToString() + " data4: " + tempts);

        /*
        //寫入VFX
        tempts = value["VFX"].ToString();
        var temptVFX = tempts == "null" ? null : ResourceAddressConfig.GetVFX(tempts);
        data.BG = temptBG;
        Debug.Log("ID: " + value["ID"].ToString() + " data4: " + tempts);
        */

        //寫入CGData
        tempts = value["CGData"].ToString();
        data.cgData = (CGData)GetResource(tempts, "cGData") ;
        data.cgBehavior = GetBehavior(tempts);

        Debug.Log("ID: " + value["ID"].ToString() + " data5: " + tempts);

        /*
        //寫入CG行為
        tempts = value["CGBehavior"].ToString();
        var temptBehavior = tempts == "null" ? DialogueSingleData.CGBehaviors.none : (DialogueSingleData.CGBehaviors)System.Enum.Parse(typeof(DialogueSingleData.CGBehaviors),tempts);
        data.cgBehavior = temptBehavior;
        Debug.Log("ID: " + value["ID"].ToString() + " data6: " + tempts);
        */
        
        //寫入Filter
        tempts = value["Filter"].ToString();

        data.filter = (Sprite)GetResource(tempts, "Filter");
        data.filterBehavior = GetBehavior(tempts);

        Debug.Log("ID: " + value["ID"].ToString() + " data5: " + tempts);

        /*
        //寫入FilterBehavior
        tempts = value["FilterBehavior"].ToString();
        temptBehavior = tempts == "null" ? DialogueSingleData.CGBehaviors.none : (DialogueSingleData.CGBehaviors)System.Enum.Parse(typeof(DialogueSingleData.CGBehaviors), tempts);
        data.filterBehavior = temptBehavior;
        Debug.Log("ID: " + value["ID"].ToString() + " data6: " + tempts);
        */

        //寫入道具
        tempts = value["Item"].ToString();

        data.iteim = (Sprite)GetResource(tempts, "Item");
        data.itemBehavior = GetBehavior(tempts);


        return data;
    }

    DialogueSingleData.CGBehaviors GetBehavior(string s)
    {
        DialogueSingleData.CGBehaviors behavior = DialogueSingleData.CGBehaviors.none;
        switch (s)
        { 
            case "null":
                break;
            case "off":
                behavior = DialogueSingleData.CGBehaviors.off;
                break;
            default:
                behavior = DialogueSingleData.CGBehaviors.on;
                break;
        }
        return behavior;
    }

    Object GetResource(string value, string type)
    {
        Object o = new Object();
        if (value == "null" || value == "off") return null; 

        switch (type)
        {
            case "BG":
                o = ResourceAddressConfig.GetBG(value);
                break;
            case "BGM":
                o = ResourceAddressConfig.GetBGM(value);
                break;
            case "SFX":
                o = ResourceAddressConfig.GetSFX(value);
                break;
            case "CGData":
                o = ResourceAddressConfig.GetCGData(value);
                break;
            case "Filter":
                o = ResourceAddressConfig.GetFilterSprite(value);
                break;
            case "Item":
                o = ResourceAddressConfig.GetItemSprite(value);
                break;
            default:
                Debug.LogError("No Item");
                break;
        }

        return o;
    }
}