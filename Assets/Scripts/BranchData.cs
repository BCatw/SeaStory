using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class OptionData
{
    public string optionName;
    public Sprite sprite;
    public string optionDescript;
    public NextData nextData;
}

[CreateAssetMenu(fileName = "BranchData_", menuName = "ScriptableObj/OptionData", order = 1)]
public class BranchData : ScriptableObject
{
    public string branchName;
    public string Question;
    public List<OptionData> optionBranches;
}