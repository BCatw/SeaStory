using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NextInfo : MonoBehaviour
{
    [Header("[Next Info]")]
    public bool isNextRefPref;
    public string nextRefPrefName;
    public NextData[] nextData;

    public NextData GetNextData()
    {
        var data = new NextData();

        return data;
    }
}

[System.Serializable]
public struct NextData
{
    public enum NextDataType { script, option, end };
    public NextDataType nextDataType;
    public string nextValue;
}