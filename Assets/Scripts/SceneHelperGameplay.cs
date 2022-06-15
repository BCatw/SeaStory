using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneHelperGameplay : MonoBehaviour
{
    static public event System.EventHandler<SceneArgs> SceneStateChanged;
    SceneArgs sceneArgs;

    void Awake()
    {
        sceneArgs = new SceneArgs();
        sceneArgs.stateNumber = 0;
    }

    public void OnGameOver()
    {
        SceneStateChanged(this, sceneArgs);
    }
}
