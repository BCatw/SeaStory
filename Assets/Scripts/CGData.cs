using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CGData_",menuName = "ScrpitableObj/CGData")][System.Serializable]
public class CGData : ScriptableObject
{
    public string cGTag;  
    public string number;
    public Sprite CG;
    public Sprite cover;
}
