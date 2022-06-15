using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectMenu : MonoBehaviour
{

    [SerializeField] SelectMenuItem[] selectMenuItems;
    [SerializeField] int countNow;
    [SerializeField] int countLast;

    void Awake()
    {
        for(int i = 0; i < selectMenuItems.Length; i++)
        {
            selectMenuItems[i].count = i;
        }
        countNow = 0;
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.UpArrow)) countNow = AddCount(-1);
        if (Input.GetKeyUp(KeyCode.DownArrow)) countNow = AddCount(1);
        if (Input.GetKeyUp(KeyCode.KeypadEnter) || Input.GetKeyUp(KeyCode.Space)) ConfirmOption();

        if(countNow != countLast)
        {
            SetSelect(countNow);
        }
    }

    void SetSelect(int count)
    {
        if(count == 0)
        {
            for (int i = 1; i < selectMenuItems.Length; i++)
            {
                selectMenuItems[i].selectedFX.SetActive(false);
            }
            return;
        }

        for (int i = 1; i < selectMenuItems.Length; i++)
        {
            if (i == count)
            {
                selectMenuItems[i].isSelected = true;
                selectMenuItems[i].selectedFX.SetActive(true);
            }
            else
            {
                selectMenuItems[i].isSelected = false;
                selectMenuItems[i].selectedFX.SetActive(false);
            }
        }
        countLast = count;
    }

    void ConfirmOption()
    {
        selectMenuItems[countNow].btn.onClick.Invoke();
    }

    int AddCount(int value)
    {
        int i = countNow + value;
        if (i > selectMenuItems.Length - 1) i = 1;
        if (i < 1) i = selectMenuItems.Length - 1;
        return i;
    }

}
