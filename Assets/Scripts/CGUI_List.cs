using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CGUI_List : MonoBehaviour
{
    [Tooltip("empty for all")]
    [SerializeField] string cGTag;
    [SerializeField] CGList cGList;
    [SerializeField] GameObject unit;
    [SerializeField] List<CGUI_Unit> cGUI_Units;

    private void Awake()
    {
        ListInstantiate();
    }

    void ListInstantiate()
    {
        Debug.Log("Is Checking CG");

        if(cGTag != "")
        {
            foreach (CGData data in cGList.cGDatas)
            {
                if (data.cGTag == cGTag)
                {
                    CGUI_Unit temptUnit = Instantiate(unit, this.transform).GetComponent<CGUI_Unit>();
                    temptUnit.WriteData(data);
                    cGUI_Units.Add(temptUnit);
                }
            }
        } else if(cGTag == "")
        {
            foreach (CGData data in cGList.cGDatas)
            {
                CGUI_Unit temptUnit = Instantiate(unit, this.transform).GetComponent<CGUI_Unit>();
                temptUnit.WriteData(data);
                cGUI_Units.Add(temptUnit);

            }
        }




    }
}
