using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 請跟DialogueLog.cs一起服用
/// </summary>
public class DialogueLogUnit : MonoBehaviour
{
    public Image avatar;
    public Text nameText;
    public Text contentText;

    public void InputDialogue(DialogueSingleData data)
    {
        nameText.text = data.characterName;
        contentText.text += data.dialogue;
    }

}
