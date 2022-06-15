using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class OptionUI : MonoBehaviour
{
    static public System.Action<bool> OptionShowEvent;
    static public System.Action<NextData> OptionChoseEvent;
    static public System.Action<string> BranchDialogueTextEvent;

    public bool isChoosing;

    [SerializeField] BranchData branchData;
    [SerializeField] List<OptionBranchUnit> optionBranchUnits;
    [SerializeField] GameObject optionUnitList;
    [SerializeField] GameObject optionUnitPrefab;
    [SerializeField] CanvasGroup canvasGroup;
    
    void Awake()
    {
        ActManager.BranchStartEvent += LoadBranchData;
        OptionBranchUnit.OptioinSelectedEvent += OnOptionChosen;
        OptionBranchUnit.MouseOnOptionEvent += OnMouseOverOption;
    }

    void OnDestroy()
    {
        ActManager.BranchStartEvent -= LoadBranchData;
        OptionBranchUnit.OptioinSelectedEvent -= OnOptionChosen;
        OptionBranchUnit.MouseOnOptionEvent -= OnMouseOverOption;
    }

    //載入分歧資料
    public void LoadBranchData(BranchData data)
    {

        Debug.Log("Option Data Name is: " + data.branchName);
        branchData = data;
        //optionUnitPrefab.SetActive(true);

        //刪除舊選項
        if (optionBranchUnits.Count > 0)
        {
            Debug.Log(optionBranchUnits.Count + " options in list");

            foreach (OptionBranchUnit unit in optionBranchUnits)
            {
                Destroy(unit.gameObject);
            }

            optionBranchUnits = new List<OptionBranchUnit>();
        }

        //生成選項
        int id = 0;

        foreach(OptionData d in data.optionBranches)
        {
            var obj = Instantiate(optionUnitPrefab, canvasGroup.transform).GetComponent<OptionBranchUnit>();
            obj.SetUnit(d);
            obj.optionID = id;
            optionBranchUnits.Add(obj);

            id++;
        }
        
        //顯示選項
        ShowOption(true);
    }
   
    //顯示關閉UI
    public void ShowOption(bool value)
    {
        isChoosing = value;
        float alpha = value? 1:0;
        float time = value ? StageController.actionTime:0;

        canvasGroup.DOFade(alpha, StageController.actionTime);
        StartCoroutine(WaitngSwitch(time, value));
    }

    //等待時間開關互動
    IEnumerator WaitngSwitch(float time, bool value)
    {
        yield return new WaitForSeconds(time);
        Debug.Log("option list <color=red>[" + value.ToString() + "]</color>");
        canvasGroup.blocksRaycasts = value;
        canvasGroup.interactable = value;
    }

    //選項選擇傳值
    public void OnOptionChosen(int value)
    {
        ShowOption(false);
        
        PlayerPrefs.SetInt(branchData.name, value);
        OptionChoseEvent(branchData.optionBranches[value].nextData);
    }

    void OnMouseOverOption(OptionData d, bool isEnter)
    {
        if (!isChoosing) return;
        string dialogue = isEnter ? d.optionDescript : branchData.Question;
        BranchDialogueTextEvent(dialogue);
    }
}