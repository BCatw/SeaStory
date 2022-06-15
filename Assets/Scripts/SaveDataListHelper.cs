using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Events;

public class SaveDataListHelper : MonoBehaviour
{
    [SerializeField] Transform content;
    [SerializeField] bool isSave;
    [SerializeField] bool isActive;
    [SerializeField] int count;
    [SerializeField] GameObject unitPrefab;
    [SerializeField] List<SaveDataUnit> dataUnit;
    [SerializeField] int selectedCount;
    [SerializeField] StageState temptState;
    [SerializeField] UnityEvent OnCoverConfirm;
    [SerializeField] UnityEvent OnLoadConfirm;

    static public System.Action<int> SaveEvent;
    static public System.Action<int> LoadEvent;

    void OnDestroy()
    { 
        SaveDataUnit.SaveUnitySelectedEvent -= OnUnitSelected;
        SaveAndLoad.RefreshUI -= RefreshList;
    }

    void Awake()
    {
        SaveDataUnit.SaveUnitySelectedEvent += OnUnitSelected;
        SaveAndLoad.RefreshUI += RefreshList;
    }

    void Start()
    {
        count = SaveAndLoad.SaveDataCount; 

        for (int i = 0; i < count; i++)
        {
            GameObject unit = Instantiate(unitPrefab, content);
            dataUnit.Add(unit.GetComponent<SaveDataUnit>());
            dataUnit[i].count = i;
        }

        RefreshList();
    }

    void OnEnable()
    {
        RefreshList();
    }
    //依照存檔刷新UI
    public void RefreshList()
    {
        Debug.Log("Refresh " + gameObject.name);
        foreach (SaveDataUnit data in dataUnit)
        {
            if (File.Exists(Application.persistentDataPath + "/SaveData" + data.count + ".save"))
            {
                Debug.Log("Loading data: " + Application.persistentDataPath + "/SaveData" + data.count + ".save");
                string jsonString = File.ReadAllText(Application.persistentDataPath + "/SaveData" + data.count + ".save");

                
                Debug.Log("json is: " + jsonString);

                
                StageState state = new StageState();
                state = StageState.CreateFromJson(jsonString);
                
                data.loadData(state);
            }
            else data.ResetUnit();
        }
    }

    void OnUnitSelected(int value)
    {
        if(this.isActiveAndEnabled) {Debug.Log(this.gameObject.name + " is recieved unit " + value);
        selectedCount = value;

            switch (isSave)
            {
                case true:
                    OnSaveUnitClicked();
                    break;

                case false:
                    OnLoadUnitClicked();
                    break;
            }
        }
    }

    void OnSaveUnitClicked()
    {
        if (File.Exists(Application.persistentDataPath + "/SaveData" + selectedCount + ".save"))
        {
            Debug.Log("this slot isn't empty");
            OnCoverConfirm.Invoke();
        }
        else
        {
            Debug.Log("this slot is empty");
            OnSave();
        }
    }

    public void OnSave()
    {
        Debug.Log("StartSave");
        SaveEvent(selectedCount);
    }

    void OnLoadUnitClicked()
    {
        if (File.Exists(Application.persistentDataPath + "/SaveData" + selectedCount + ".save"))
        {
            Debug.Log("You sure load this?");
            OnLoadConfirm.Invoke();
        }
        else Debug.Log("this slot is empty");

    }

    public void Load()
    {
        Debug.Log("StartLoad");
        LoadEvent(selectedCount);
    }
}