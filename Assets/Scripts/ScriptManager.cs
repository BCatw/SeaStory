using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptManager : MonoBehaviour
{   
    static public event System.EventHandler<DialogueSingleData> DialogueDoneHandler;

    //資料
    static public DialogueData dialogueDataPool { get; set; }
    static public int countDialogue;

    static public void LoadScript(string value)
    {
        dialogueDataPool = Resources.Load<DialogueData>("ScriptableObj/DialoguePool_"+value);
    }

/*    public Sprite GetBG()
    {
        return dialogueDataPool.BG;
    }
*/    
    public DialogueSingleData GetDialogueData(int count)
    {
        DialogueSingleData dd = dialogueDataPool.dialogueSingleDatas[count];
        return dd;
    }
}