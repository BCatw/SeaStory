using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Common_UI_Black : MonoBehaviour
{
    [SerializeField] UnityEvent OnGetValue;
    [SerializeField] UnityEvent SendBackEvent;

    object target;
    string message;

    // Start is called before the first frame update
    void GetValue(object sender, string value)
    {
        target = sender;
        message = value;
    }

    public void SendValue()
    {
        
    }
}
