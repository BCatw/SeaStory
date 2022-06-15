using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Covin19Demo : MonoBehaviour
{
    static public System.Action BackTitleEvent;
    [SerializeField] UnityEvent OnCall;

    void Awake()
    {
        ActManager.CallPopUpEvent += OnCalled;
    }

    void OnDestroy()
    {
        ActManager.CallPopUpEvent -= OnCalled;
    }

    void OnShowPopUp(UnityEvent unityEvent)
    {

    }

    void OnCalled()
    {
        OnCall.Invoke();
    }

    public void OnBtnClicked(string value)
    {
        switch (value)
        {
            case "survey":
                Application.OpenURL("https://www.surveycake.com/s/BQNKr");
                break;
            case "title":
                BackTitleEvent();
                break;
        }
    }

}
