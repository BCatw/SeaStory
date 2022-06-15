using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public struct RecieveAction
{
    public string tag;
    public UnityEvent action;
}

public class CrossSceneReciever : MonoBehaviour
{
    [SerializeField] RecieveAction[] recieveActions;

    void Awake()
    {
        CrossSceneSender.send += Recieve;
    }

    void OnDestroy()
    {
        CrossSceneSender.send -= Recieve;
    }

    public void Recieve(string value)
    {
        Debug.Log("[" + this.gameObject.scene.name + "] Tag " + value + " is recieved");
        for (int i = 0; i < recieveActions.Length; i++)
        {
            if (value == recieveActions[i].tag)
            {
                Debug.Log("[" + this.gameObject.scene.name + "] Tag " + value + " is invoked");
                recieveActions[i].action.Invoke();
            }
        }
    }
}
