using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "ScriptableObj/CharacterData", order = 1)]
public class CharacterData : ScriptableObject
{
    public string characterName;
    public Sprite[] sprite;
}