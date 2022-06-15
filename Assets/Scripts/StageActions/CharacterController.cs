using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterState
{
    public string name;
    public string sprite;
    public float posit;
    public bool isOn;
    public bool isFacingRight;
}

public class CharacterAnimation
{
    public enum AnimationType { none, shake };
    public AnimationType animationType;

}
public class CharacterController : MonoBehaviour
{
    static public System.Action PrepareDoneEvent;
    static public System.Action<string, Sprite> ChangeSpriteEvent;
    static public System.Action<string, Sprite> CharacterOnEvent;
    static public System.Action<string> CharacterOffEvent;
    static public System.Action<string, Vector2> CharacterMoveEvent;
    static public System.Action<string, CharacterAnimation> CharacterAnimationEvent;

    [SerializeField] List<CharacterUnit> characterUnits;
    [SerializeField] GameObject characterPrefab;
    [SerializeField] Transform charactersParent;
    [SerializeField] float positMax;

    void Awake()
    {
        ActManager.ChangeScriptEvent += SetCharacter;
        ActManager.CharacterActEvent += OnRecieve;
        SetCharacter();
    }

    void OnDestroy()
    {
        ActManager.ChangeScriptEvent -= SetCharacter;
        ActManager.CharacterActEvent -= OnRecieve;
    }

    public void SetCharacter()
    {
        //清空角色列表
        if (characterUnits.Count > 0)
        {
            DeletCharacter();
        }
        
        ScriptData scriptData = ActManager.scriptData;
        
        //生成物件並列入list
        foreach (string character in scriptData.characterName)
        {
            var unit = Instantiate(characterPrefab, charactersParent).GetComponent<CharacterUnit>();
            unit.characterName = character;
            characterUnits.Add(unit);
            
            unit.spriteHelper.SetLayer(SpriteLayerConfig.CharacterLayer + characterUnits.Count);
        }

        //調整角色狀態
        switch(ActManager.isLoadStageState){

            case false:
                Debug.Log("LSS false");
                if (scriptData.startCharacters.Length > 0)
                {
                    foreach (StartCharacter chara in scriptData.startCharacters)
                    {
                        CharacterAct act = new CharacterAct();

                        act.characterName = chara.characterName;
                        act.sprite = chara.characterSprite;
                        act.xPosition = chara.startPositX;
                        act.isMoving = true;
                        act.immediatelyMoving = true;
                        act.spriteAct = CharacterAct.SpriteAct.On;

                        OnAct(act);
                    }
                }
                break;

            case true:
                Debug.Log("LSS true");
                foreach (CharacterState state in ActManager.stageState.characterStates)
                {
                    CharacterAct act = new CharacterAct();

                    act.characterName = state.name;
                    act.sprite = ResourceAddressConfig.GetCharacterSprite(state.sprite);
                    act.xPosition = state.posit;
                    act.isMoving = true;
                    act.immediatelyMoving = true;
                    act.spriteAct = state.isOn? CharacterAct.SpriteAct.On: CharacterAct.SpriteAct.Off;

                    OnAct(act);
                }
                
                break;
        }
    }

    public void DeletCharacter()
    {
        foreach (CharacterUnit unit in characterUnits)
        {
            if (unit != null)
            {
                Destroy(unit.gameObject);
            }
        }
        characterUnits.Clear();
    }

    void OnRecieve(CharacterAct[] characterActs)
    {
        foreach(CharacterAct act in characterActs){
            OnAct(act);
        }
    }

    void OnAct(CharacterAct characterAct)
    {
        CharacterUnit temptcharacter = null;

        for (int i = 0; i < characterUnits.Count; i++)
        {
            if (characterUnits[i].characterName == characterAct.characterName) temptcharacter = characterUnits[i];
        }

        temptcharacter.isFacingRight = characterAct.isFacingRight;

        switch (characterAct.spriteAct)
        {
            case CharacterAct.SpriteAct.Chamge:
                temptcharacter.OnCharacterSpriteChange(characterAct.sprite);
                break;

            case CharacterAct.SpriteAct.On:
                temptcharacter.OnCharacterOn(characterAct.sprite);
                break;

            case CharacterAct.SpriteAct.Off:
                temptcharacter.OnCharacterOff();
                break;

            case CharacterAct.SpriteAct.none:
                break;
        }

        if (characterAct.isMoving)
        {
            var time = characterAct.immediatelyMoving ? 0.0f : StageController.actionTime;
            temptcharacter.OnCharacterMove(characterAct.xPosition * positMax, time);
        }

        if(characterAct.layer != 0)
        {
            temptcharacter.spriteHelper.SetLayer(characterAct.layer);
        }

        if(characterAct.animationType != CharacterAnimation.AnimationType.none)
        {
            temptcharacter.Animate(characterAct.animationType);
        }
    }
    public List<CharacterState> BakeCharacter()
    {
        List<CharacterState> list = new List<CharacterState>();
        foreach(CharacterUnit unit in characterUnits)
        {
            CharacterState state = new CharacterState();
            state.name = unit.characterName;
            state.sprite = unit.GetSpriteNow().name;
            state.posit = unit.GetXPosit()/positMax;
            state.isOn = unit.isOn;
            list.Add(state);
        }

        return list;
    }
}
