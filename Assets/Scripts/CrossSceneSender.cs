using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CrossSceneSender : MonoBehaviour
{
    static public System.Action<string> send;

    public void Send(string value)
    {
        Debug.Log("[" + this.gameObject.scene.name + "] Tag " + value + " is sended");
        send.Invoke(value);
    }
}
