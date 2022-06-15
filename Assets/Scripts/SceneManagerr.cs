using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

[System.Serializable]
public struct SceneState
{
    public string sateName;
    public string[] sceneNames;
}

public class SceneArgs: System.EventArgs
{
    public int stateNumber;
}

public class SceneManagerr : MonoBehaviour
{
    [SerializeField] SceneState[] sceneStates;
    [SerializeField] int sceneSateNow;
    [SerializeField] int sceneSateLast;

    [SerializeField] UnityEvent OnChangeStart;
    [SerializeField] UnityEvent OnChangeDone;

    object senderNow;
    object senderLast;

    void Awake()
    {
        LoadSceneSet(0);
        ActManager.PrepareDone += OnSceneDone;
        ActManager.SceneStateChanged += RecieveStateData;
        SceneHelperTitle.PrepareDoneEvent += OnSceneDone;
        EndingHelper.SceneStateChanged += RecieveStateData;
    }

    //接收切換資訊
    public void RecieveStateData(object sender, SceneArgs a)
    {
        OnChangeStart.Invoke();

        sceneSateLast = sceneSateNow;
        senderLast = senderNow;

        sceneSateNow = a.stateNumber;
        senderNow = sender;
    }

    //載入、卸載場景，換場動畫結束後呼叫
    public void ChangeMultiScene()
    {
        LoadSceneSet(sceneSateNow);
        UnloadSceneSet(sceneSateLast);
    }

    //當載入完成後，關閉換場動畫
    public void OnSceneDone()
    {
        OnChangeDone.Invoke();
    }

    void LoadSceneSet(int value)
    {
        SceneState set = sceneStates[value];

        for (int i = 0; i < set.sceneNames.Length; i++)
        {
            SceneManager.LoadScene(set.sceneNames[i], LoadSceneMode.Additive);
        }
        
        switch (value)
        {
            case 0:
                SceneHelperTitle.SceneStateChanged += RecieveStateData;
                break;
            case 1:
                SceneHelperGameplay.SceneStateChanged += RecieveStateData;
                break;
        }
    }

    void UnloadSceneSet(int value)
    {
        SceneState set = sceneStates[value];
        for (int i = 0; i < set.sceneNames.Length; i++)
        {
            SceneManager.UnloadSceneAsync(set.sceneNames[i]);
        }

        switch (value)
        {
            case 0:
                SceneHelperTitle.SceneStateChanged -= RecieveStateData;
                break;
            case 1:
                SceneHelperGameplay.SceneStateChanged -= RecieveStateData;
                break;
        }
    }
}
