using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SelectMenuItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Button btn;
    public GameObject selectedFX;
    public int count;
    public bool isSelected;
    public bool isMouseOver;

    void Awake()
    {
        btn = GetComponent<Button>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isMouseOver = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isMouseOver = false;
    }

}
