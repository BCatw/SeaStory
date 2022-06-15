using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class SaveDataUnit : MonoBehaviour
{
    public int count;
    [SerializeField] Image imagine;
    [SerializeField] Text sceneText;
    [SerializeField] Text dateText;
    [SerializeField] Text countText;
    [SerializeField] Sprite defaultSprite;

    public static System.Action<int> SaveUnitySelectedEvent;

    public void loadData(StageState state)
    {
        ScriptData script = ResourceAddressConfig.GetScriptData(state.script);
        string[] chapterNumber = script.excelFileName.Split('_');
        imagine.sprite = script.cover;
        sceneText.text = chapterNumber[1] + ". " + script.sceneName;
        dateText.text = state.saveDate;
        countText.text = "" + state.count;
    }

    public void ResetUnit()
    {
        imagine.sprite = defaultSprite;
        sceneText.text = "";
        countText.text = "";
        dateText.text = "";
    }

    public void OnClicked()
    {
        SaveUnitySelectedEvent(count);
    }
}
