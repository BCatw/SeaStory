using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OptionBranchUnit : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    static public System.Action<int> OptioinSelectedEvent;
    static public System.Action<OptionData, bool> MouseOnOptionEvent;

    public int optionID;

    OptionData data;

    [SerializeField] Button button;
    [SerializeField]public Text optionNameText;
    [SerializeField] Image optionImage;

    void Awake()
    {
        button = this.GetComponent<Button>();
        button.onClick.AddListener(OnBtnClick);
    }
    
    public void OnBtnClick()
    {
        OptioinSelectedEvent(optionID);
    }

    public void SetUnit(OptionData d)
    {
        data = d;
        optionNameText.text = data.optionName;
        optionImage.sprite = data.sprite;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        MouseOnOptionEvent(data, true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        MouseOnOptionEvent(data, false);
    }
}