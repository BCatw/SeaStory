using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// 由excel檔管理遊戲中所有的CG
/// 並且管理各個CG解鎖進度
/// </summary>

[CreateAssetMenu(fileName = "Script", menuName = "ScriptableObj/CGList", order = 1)]
public class CGList : ScriptableObject
{
    [SerializeField] string excelFileName;
    public List<CGData> cGDatas;
    static public string playerprefTagFormate = "CGUnlock_{0}_{1}";

#if UNITY_EDITOR

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

        for (var i = 0; i < data.Count; i++)
        {
            string filename = ("CGData_" + data[i]["CharacterName"].ToString() + "_" + data[i]["Chapter"].ToString() + data[i]["Branch"].ToString() + data[i]["ID"].ToString());
            Debug.Log("CGData_" + data[i]["CharacterName"].ToString() + "_" + data[i]["Chapter"].ToString() + data[i]["Branch"].ToString() + data[i]["ID"].ToString());

            bool isExisted = false;
            CGData temptCGdata = ScriptableObject.CreateInstance<CGData>();

            foreach (CGData cgdata in cGDatas)
            {
                if (cgdata.name == filename)
                {
                    Debug.Log(filename + " is Exidted");
                    OverridData(data[i], cgdata);
                    isExisted = true;
                    temptCGdata = cgdata;
                    break;
                }
            }

            if (!isExisted)
            {
                Debug.Log(filename + " isn't Exidted");
                AssetDatabase.CreateAsset(temptCGdata, "Assets/Resources/" + filename + ".asset");
                AssetDatabase.SaveAssets();
                cGDatas.Add(temptCGdata);
            }

            OverridData(data[i], temptCGdata);
            EditorUtility.SetDirty(this);
        }       
    }
#endif

    public void OverridData(Dictionary<string, object> inputdata, CGData overrideData)
    {
        //寫入角色名稱
        overrideData.cGTag = inputdata["CharacterName"].ToString();

        //寫入
        overrideData.number = inputdata["Chapter"].ToString() + inputdata["Branch"].ToString() + inputdata["ID"].ToString();

        //建立playerpref
        string tag = string.Format(playerprefTagFormate, overrideData.cGTag, overrideData.number);
        if (PlayerPrefs.GetInt(tag, 0) != 0) Debug.Log(tag + " is already unlocked");
    
        //寫入
        string filename = ("CG_" + overrideData.cGTag + "_" + overrideData.number);
        Debug.Log(overrideData.name + " 's sprite name is " + filename);
        Sprite sprite = Resources.Load<Sprite>("CG/" + filename);
        Sprite cover = Resources.Load<Sprite>("CG/" + filename+"_Cover");

        overrideData.CG = sprite;
        overrideData.cover = cover;
    }

    static public bool isUnlock(CGData data)
    {
        bool unlock = false;

        string tag = string.Format(playerprefTagFormate, data.cGTag, data.number);
        int value = PlayerPrefs.GetInt(tag);

        unlock = value != 0 ? true : false;

        return unlock;
    }

    static public void CGUnlock(CGData data)
    {
        string tag = string.Format(playerprefTagFormate, data.cGTag, data.number);
        PlayerPrefs.SetInt(tag, 1);
    }
}
