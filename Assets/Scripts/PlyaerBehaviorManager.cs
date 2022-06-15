using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlyaerBehaviorManager : MonoBehaviour
{
    static public System.Action ContinueBehavior;
    static public System.Action AutoBehavior;
    static public System.Action<bool> SkipBehavior;
    static public System.Action<bool> LogUIBehavior;
    static public System.Action LogUIOneKeyBehavior;
    static public System.Action MenuUIBehavior;
    static public System.Action HideUIBehavior;

    void Update()
    {
        if (Input.GetKeyDown(KeyCodeConfig.keyContinue) || Input.GetKeyDown(KeyCodeConfig.keyContinueAlt)) ContinueBehavior();

        if (Input.GetKeyDown(KeyCodeConfig.keyAuto)) AutoBehavior();

        if (Input.GetKeyDown(KeyCodeConfig.keySkip)) SkipBehavior(true);
        else if (Input.GetKeyUp(KeyCodeConfig.keySkip)) SkipBehavior(false);

        if (Input.mouseScrollDelta.y > 0) LogUIBehavior(true);
        else if (Input.mouseScrollDelta.y < 0) LogUIBehavior(false);
        if (Input.GetKeyDown(KeyCodeConfig.KeyLog)) LogUIOneKeyBehavior();

        if (Input.GetKeyDown(KeyCodeConfig.keyMenu)) MenuUIBehavior();

        if (Input.GetKeyDown(KeyCodeConfig.keyHide) || Input.GetKeyDown(KeyCodeConfig.KeyHideAlt)) HideUIBehavior();
    }


}
