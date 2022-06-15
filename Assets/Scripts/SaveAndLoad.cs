using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveAndLoad : MonoBehaviour
{    
    [SerializeField] ActManager actManager;
    [SerializeField] SceneManagerr sceneManagerr;
    [SerializeField] int saveDataCount;
    int selectedData;
    string loadDataString;
    StageState loadedData;
    static public int SaveDataCount;

    static public System.Action RequireBakeEvent;
    static public System.Action saveEvent;
    static public System.Action<StageState> loadSceneEvent;
    static public System.Action RefreshUI;

    private void Awake()
    {
        StageBaker.SendBakeEvent += OnGetBake;
        SaveDataListHelper.SaveEvent += OnSaveStart;
        SaveDataListHelper.LoadEvent += OnLoadData;
        SaveDataCount = saveDataCount;
    }

    //確定存檔時，儲存玩家選項、跟Baker要State
    public void OnSaveStart(int saveCount)
    {
        selectedData = saveCount;
        RequireBakeEvent();
    }

    //從Baker拿到State
    public void OnGetBake(StageState state)
    {
        state.script = actManager.GetRecentScript().excelFileName;
        state.count = actManager.processNow >= actManager.processMax ? actManager.processNow - 1: actManager.processNow;
        OnSave(selectedData, state);
    }

    //存檔實做
    public void OnSave(int saveCount, StageState bakeState)
    {
        string date = System.DateTime.Now.ToString("yyyy年MM月dd日 HH:mm:ss");
        bakeState.saveDate = date;
        string jsonContent = JsonUtility.ToJson(bakeState);

        FileStream fs = File.Create(Application.persistentDataPath + "/SaveData" + saveCount + ".save");
        StreamWriter ws = new StreamWriter(fs);
        ws.Write(jsonContent);
        ws.Close();
        fs.Close();
        
        /*
        File.Create(Application.persistentDataPath + "/SaveData/SaveData" + saveCount);
        File.WriteAllText(Application.persistentDataPath + "/SaveData/SaveData" + saveCount, jsonContent);
        */

        Debug.Log("Save Success" + File.Exists(Application.persistentDataPath + "/SaveData" + saveCount + ".save") );
        RefreshUI();
    }

    //依照玩家選擇讀檔
    public void OnLoadData(int saveCount)
    {
        ActManager.isLoadStageState = true;
        loadDataString = File.ReadAllText(Application.persistentDataPath + "/SaveData" + saveCount + ".save");
        loadedData = JsonUtility.FromJson<StageState>(loadDataString);
        //loadSceneEvent(loadedData);
        
        PlayerPrefs.SetString("LoadingScript", loadedData.script);
        PlayerPrefs.SetInt("ProcessStart", loadedData.count);
        actManager.LoadStageState(loadedData);

        SceneArgs sceneArgs = new SceneArgs();
        sceneArgs.stateNumber = 1;

        sceneManagerr.RecieveStateData(this, sceneArgs);
    }
}